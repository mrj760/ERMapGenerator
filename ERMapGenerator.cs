using System.Globalization;
using System.Xml;
using DarkModeForms;
using DdsFileTypePlus;
using ImageMagick;
using MetroFramework.Forms;
using PaintDotNet;
using SoulsFormats;

namespace ERMapGenerator;

public partial class ERMapGenerator : MetroForm
{
    private const string version = "1.3";

    // private const float mapDisplayMaxZoomLevel = 2.0f;
    // private const float mapDisplayZoomIncrement = 0.1f;
    private static string gameModFolderPath = "";
    private static string mapTileMaskBndPath = "";
    private static string mapTileTpfBhdPath = "";
    private static string mapTileTpfBtdPath = "";
    private static string outputFolderPath = "";
    private static string inputFolderPath = "";
    private static BND4 mapTileMaskBnd = new();
    private static BXF4 mapTileTpfBhd = new();
    private static Matrix Flags = new();

    private static XmlNode? MapTileMaskRoot;

    // private static bool isDraggingMapDisplay;
    // private static int mapDisplayXPos;
    // private static int mapDisplayYPos;
    private static BXF4 mapTileBhd = new();

    // private float mapDisplayMinZoomLevel = -1;
    // private float mapDisplayZoomLevel;
    private Bitmap? savedMapImage = null!;

    private DarkModeCS dm;

    public ERMapGenerator()
    {
        InitializeComponent();
        dm = new DarkModeCS(this)
        {
            ColorMode = DarkModeCS.DisplayMode.SystemDefault
        };
        SetVersionString();
        CenterToScreen();
        RegisterFormEvents();
    }

    private void RegisterFormEvents()
    {
        // mapDisplayPictureBox.MouseWheel += MapDisplayPictureBox_MouseWheel;
    }

    private void SetVersionString()
    {
        versionStr.Text += $@" {version}";
    }

    private void ERMapGenerator_Shown(object sender, EventArgs e)
    {
        // mapDisplayGroupBox.Enabled = false;
        mapConfigurationGroupBox.Enabled = false;
        outputFolderGroupBox.Enabled = false;
        drawTileDebugInfoCheckBox.Enabled = false;
        automateButton.Enabled = true;
        InputFolderGroupBox.Enabled = false;
    }

    private static string[] GetAllFolderFiles(string folderPath, string fileType = "*.*")
    {
        try
        {
            return Directory.GetFiles(folderPath, fileType, SearchOption.AllDirectories);
        }
        catch (Exception)
        {
            return Array.Empty<string>();
        }
    }

    private static void ShowInformationDialog(string str)
    {
        MessageBox.Show(str, @"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private static bool ResourceExists(string path, string dispName)
    {
        bool doesExist = Path.HasExtension(path) && File.Exists(path) || Directory.Exists(path);
        if (!doesExist)
            ShowInformationDialog($"{dispName} wasn't found. "
                                  + "Please ensure it's located in the menu folder in your game/mod files.");
        return doesExist;
    }

    private void BrowseGameModFolderButton_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog dialog = new()
        {
            Description = @"Open Game/Mod Folder",
            UseDescriptionForTitle = true
        };
        if (dialog.ShowDialog() != DialogResult.OK) return;
        gameModFolderPath = dialog.SelectedPath;
        string[] gameModFolderFiles = GetAllFolderFiles(gameModFolderPath);
        mapTileMaskBndPath = gameModFolderFiles.FirstOrDefault(i => i.Contains(".mtmskbnd.dcx")) ?? "";
        mapTileTpfBhdPath = gameModFolderFiles.FirstOrDefault(i => i.Contains("71_maptile.tpfbhd")) ?? "";
        mapTileTpfBtdPath = mapTileTpfBhdPath.Replace(".tpfbhd", ".tpfbdt");
        if (!ResourceExists(mapTileMaskBndPath, "71_maptile.mtmskbnd.dcx")) return;
        if (!ResourceExists(mapTileTpfBhdPath, "71_maptile.tpfbhd")) return;
        if (!ResourceExists(mapTileTpfBtdPath, "71_maptile.tpfbdt")) return;
        gameModFolderPathLabel.Text = Path.GetDirectoryName(mapTileTpfBhdPath);
        mapTileMaskBnd = BND4.Read(mapTileMaskBndPath);
        mapTileTpfBhd = BXF4.Read(mapTileTpfBhdPath, mapTileTpfBtdPath);
        outputFolderGroupBox.Enabled = true;
        InputFolderGroupBox.Enabled = true;
    }

    private void OutputFolderButton_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog dialog = new();
        if (dialog.ShowDialog() != DialogResult.OK) return;
        outputFolderPath = dialog.SelectedPath;
        outputFolderPathLabel.Text = outputFolderPath;
        // TODO: Cleanup
        drawTileDebugInfoCheckBox.Enabled = true;
        PopulateGroundLevels();
        PopulateZoomLevels();
    }

    private static MagickImage CreateMapGrid(int gridSizeX, int gridSizeY, int tileSize)
    {
        return new MagickImage(MagickColors.Black, tileSize * gridSizeX, tileSize * gridSizeY);
    }

    // private static void SetFlags(int zoomLevel)
    // {
    //     Flags = new Matrix();
    //     for (int i = 0; i < MapTileMaskRoot?.ChildNodes.Count; ++i)
    //     {
    //         XmlNode? node = MapTileMaskRoot.ChildNodes[i];
    //         if (node == null)
    //         {
    //             ShowInformationDialog($@"Coordinate node {i} does not exist.");
    //             continue;
    //         }
    //
    //         if (node.Attributes == null || node.Attributes.Count < 2)
    //         {
    //             ShowInformationDialog($@"Coordinate node {i} does not contain any attribute information.");
    //             continue;
    //         }
    //
    //         string coord = $"{int.Parse(node.Attributes[1].Value):00000}";
    //
    //         bool isValid = zoomLevel switch
    //         {
    //             0 => coord.StartsWith("0"),
    //             1 => coord.StartsWith("1"),
    //             2 => coord.StartsWith("2"),
    //             _ => false
    //         };
    //         if (!isValid) continue;
    //         int x = int.Parse(coord.Substring(1, 2));
    //         int y = int.Parse(coord.Substring(3, 2));
    //         int mask = int.Parse(node.Attributes[2].Value);
    //         Flags[-1, x, y] = mask;
    //     }
    // }

    private static Set<int> CollectMasks()
    {
        var masks = new Set<int>();
        for (int i = 0; i < MapTileMaskRoot?.ChildNodes.Count; ++i)
        {
            var node = MapTileMaskRoot.ChildNodes[i];
            if (node == null)
            {
                ShowInformationDialog($@"Coordinate node {i} does not exist.");
                continue;
            }

            if (node.Attributes == null || node.Attributes.Count < 2)
            {
                ShowInformationDialog($@"Coordinate node {i} does not contain any attribute information.");
                continue;
            }

            int mask = int.Parse(node.Attributes[2].Value);
            if (!masks.Contains(mask))
            {
                masks.Add(mask);
            }
        }

        return masks;
    }

    private static void SetFlagsForExportTiles(int mask)
    {
        Flags = new Matrix();
        for (int i = 0; i < MapTileMaskRoot?.ChildNodes.Count; ++i)
        {
            var node = MapTileMaskRoot.ChildNodes[i];
            if (node == null)
            {
                ShowInformationDialog($@"Coordinate node {i} does not exist.");
                continue;
            }

            if (node.Attributes == null || node.Attributes.Count < 2)
            {
                ShowInformationDialog($@"Coordinate node {i} does not contain any attribute information.");
                continue;
            }

            string coord = $"{int.Parse(node.Attributes[1].Value):00000}";
            int zoomLevel = int.Parse(coord[..1]);
            int x = int.Parse(coord.Substring(1, 2));
            int y = int.Parse(coord.Substring(3, 2));
            bool use = int.Parse(node.Attributes[2].Value) == mask;
            Flags[zoomLevel, x, y] = use;
        }
    }

    private void ReadMapTileMaskRoot(string groundLevel)
    {
        BinderFile? file = mapTileMaskBnd.Files.Find(i => i.Name.Contains(groundLevel));
        if (file != null)
        {
            string fileName = Path.GetFileName(file.Name);
            progressLabel.Invoke(new Action(() => progressLabel.Text = $@"Parsing map tile mask {fileName}..."));
            XmlDocument doc = new();
            doc.Load(new MemoryStream(file.Bytes));
            MapTileMaskRoot = doc.LastChild;
            if (MapTileMaskRoot == null) ShowInformationDialog($@"{fileName} contains no root XML node.");
            // else SetFlags(0);
        }
    }

    private async Task UnpackMaps()
    {
        List<string> groundLevels = GetGroundLevels();
        List<string> zoomLevels = GetZoomLevels().Keys.ToList();
        int groundLevelIndex = groundLevelComboBox.Invoke(() => groundLevelComboBox.SelectedIndex);
        int zoomLevelIndex = zoomLevelComboBox.Invoke(() => zoomLevelComboBox.SelectedIndex);
        groundLevels = GetFilteredGroundLevels(groundLevelIndex, groundLevels).ToList();
        zoomLevels = GetFilteredZoomLevels(zoomLevelIndex, zoomLevels).ToList();
        foreach (string groundLevel in groundLevels)
        {
            foreach (string zoomLevel in zoomLevels)
                await UnpackStitchMap(groundLevel, zoomLevel);
        }
    }

    private async Task UnpackStitchMap(string groundLevel = "M00", string zoomLevel = "L0")
    {
        await progressLabel.InvokeAsync(new Action(() => progressLabel.Text = $@"Parsing texture files..."));
        if (string.IsNullOrEmpty(outputFolderPath)) return;
        int gridSizeX = GetZoomLevels().GetValueOrDefault(zoomLevel);
        const int tileSize = 256;

        ReadMapTileMaskRoot(groundLevel);
        foreach (int mask in CollectMasks())
        {
            bool zoomLevelReached = false;
            var grid = CreateMapGrid(gridSizeX, gridSizeX, tileSize);
            string? outFileName = null;
            foreach (var tpfFile in mapTileTpfBhd.Files /* Sorted alphabetically already */)
            {
                var texFile = TPF.Read(tpfFile.Bytes).Textures[0];
                string[] tokens = texFile.Name.Split('_');

                if (mask != int.Parse(tokens[6], NumberStyles.HexNumber) ||
                    !tokens[0].ToLower().Equals("menu") ||
                    !tokens[1].ToLower().Equals("maptile")) continue;

                string groundLevelToken = tokens[2];
                string zoomLevelToken = tokens[3];

                /* Since sorted alphabetically, this indicates further iterations are irrelevant */
                if (zoomLevelReached && zoomLevelToken != zoomLevel) break;

                /* Conversely, this indicates we haven't reached the desired ground/zoom level yet */
                if (groundLevelToken != groundLevel || zoomLevelToken != zoomLevel) continue;
                zoomLevelReached = true;

                int x = int.Parse(tokens[4]);
                int y = int.Parse(tokens[5]);

                if (outFileName == null)
                {
                    outFileName = string.Join(
                        "_", tokens[0], tokens[1], groundLevelToken, zoomLevelToken, mask.ToString("X8"));
                }

                grid.BackgroundColor = MagickColors.Transparent;

                await progressLabel.InvokeAsync(
                    new Action(() => progressLabel.Text = $@"Stitching texture file {texFile.Name}..."));

                MagickImage tile = new(texFile.Bytes);
                tile.Resize(tileSize, tileSize);

                int adjustedX = x * tileSize;
                int adjustedY = grid.Height - y * tileSize - tileSize;

                grid.Draw(new Drawables().Composite(adjustedX, adjustedY, tile));
            }

            if (outFileName == null) continue;
            await progressLabel.InvokeAsync(new Action(() =>
                progressLabel.Text = $@"Writing texture file {outFileName}..."));
            await WriteStitchedMap(grid, outFileName);
        }
    }

    private async Task WriteStitchedMap(IMagickImage grid, string path)
    {
        string outputFileName = $"{path}.dds";
        string outputFilePath = $"{outputFolderPath}\\{outputFileName}";
        await progressLabel.InvokeAsync(new Action(() => progressLabel.Text = $@"Writing {outputFileName} to file..."));
        await Task.Delay(1000);
        await grid.WriteAsync(outputFilePath);
    }

    private async Task RepackTileMap()
    {
        String info = "";
        foreach (string inputFile in Directory.GetFiles(inputFolderPath, "*.dds"))
        {
            string filename = Path.GetFileNameWithoutExtension(inputFile);
            string[] tokens = filename.Split("_");
            if (tokens.Length < 3) continue;
            if ((savedMapImage = LoadMapImage(inputFile)) == null)
            {
                info += $"The image {filename} could not be read. \n";
                continue;
            }

            string groundLevel = tokens[^3];
            string zoomLevel = tokens[^2];
            string mask = tokens[^1];
            await ExportTiles(groundLevel, zoomLevel, mask);
        }

        mapTileBhd = new BXF4();
        ShowInformationDialog("Export Complete.\n" + info);
    }

    private Task ExportTiles(string groundLevel, string zoomLevel, string mask)
    {
        if (savedMapImage == null) return Task.CompletedTask;
        int gridSize = GetZoomLevels().GetValueOrDefault(zoomLevel);
        const int tileSize = 256;
        using Bitmap mapImage = new(savedMapImage);
        ReadMapTileMaskRoot(groundLevel);
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Rectangle tileRect = new(
                    x * tileSize,
                    y * tileSize,
                    Math.Min(tileSize, mapImage.Width - x * tileSize),
                    Math.Min(tileSize, mapImage.Height - y * tileSize)
                );
                using Bitmap tileImage = new(tileSize, tileSize);
                using (var g = Graphics.FromImage(tileImage))
                {
                    g.Clear(Color.Transparent);
                    g.DrawImage(mapImage, new Rectangle(0, 0, tileSize, tileSize), tileRect, GraphicsUnit.Pixel);
                }

                string tileXPos = x.ToString("D2");
                string tileYPos = (gridSize - y - 1).ToString("D2");
                string newTileName = $"MENU_MapTile_{groundLevel}_{zoomLevel}_{tileXPos}_{tileYPos}_{mask:X8}";
                WriteTile(tileImage, newTileName);
            }
        }

        var files = mapTileBhd.Files.Select(file =>
            mapTileTpfBhd.Files.FindIndex(i =>
                string.Equals(i.Name, file.Name, StringComparison.OrdinalIgnoreCase)
            )
        );
        foreach (int i in files.Where(index => index != -1)) mapTileTpfBhd.Files.RemoveAt(i);
        mapTileTpfBhd.Files.AddRange(mapTileBhd.Files);
        mapTileTpfBhd.Files = mapTileTpfBhd.Files.OrderBy(i => i.Name).ToList();
        for (int i = 0; i < mapTileTpfBhd.Files.Count; i++) mapTileTpfBhd.Files[i].ID = i;
        mapTileTpfBhd.Write(mapTileTpfBhdPath, mapTileTpfBtdPath);

        return Task.CompletedTask;
    }

    private IEnumerable<string> GetFilteredGroundLevels(int groundLevelIndex, IReadOnlyList<string> groundLevels)
    {
        return automationModeTabControl.Invoke(() => automationModeTabControl.SelectedIndex == 0)
            ? groundLevelIndex == 0 ? groundLevels.Skip(1) : [groundLevels[groundLevelIndex]]
            : [groundLevels[groundLevelIndex]];
    }

    private IEnumerable<string> GetFilteredZoomLevels(int zoomLevelIndex, IReadOnlyList<string> zoomLevels)
    {
        // TODO: Function
        return automationModeTabControl.Invoke(() => automationModeTabControl.SelectedIndex == 0)
            ? zoomLevelIndex == 0 ? zoomLevels.Skip(1) : new[] { zoomLevels[zoomLevelIndex] }
            : new[] { zoomLevels[zoomLevelIndex] };
    }

    private void WriteTile(IDisposable tileImage, string tileName)
    {
        progressLabel.Invoke(() => progressLabel.Text = $@"Writing {tileName}...");
        TPF.Texture texture = new();
        byte[] bytes = (byte[])new ImageConverter().ConvertTo(tileImage, typeof(byte[]))!;
        IMagickImage<ushort> image = MagickImage.FromBase64(Convert.ToBase64String(bytes));
        texture.Bytes = ConvertMagickImageToDDS(image);
        texture.Name = tileName;
        texture.Format = 0x66;
        TPF tpf = new() { Compression = DCX.Type.DCX_DFLT_11000_44_9_15 };
        tpf.Textures.Add(texture);
        byte[] tpfBytes = tpf.Write();
        BinderFile file = new()
        {
            Name = $"71_MapTile\\{tileName}.tpf.dcx",
            Bytes = tpfBytes
        };
        mapTileBhd.Files.Add(file);
    }

    private void ToggleAllControls(bool wantsEnabled)
    {
        // RefreshMapImage();
        mapConfigurationGroupBox.Enabled = wantsEnabled;
        automationModeTabControl.Enabled = wantsEnabled;
        automateButton.Enabled = wantsEnabled;
        gameModFolderGroupBox.Enabled = wantsEnabled;
    }

    private async void AutomateButton_Click(object sender, EventArgs e)
    {
        ToggleAllControls(false);
        if (automationModeTabControl.SelectedIndex == 0) await Task.Run(UnpackMaps);
        else await Task.Run(RepackTileMap);
        progressLabel.Invoke(new Action(() => progressLabel.Text = @"Automation complete!"));
        await Task.Delay(1000);
        progressLabel.Text = @"Waiting...";
        ToggleAllControls(true);
    }

    private void UpdateMapDisplayMinZoomLevel()
    {
        // mapDisplayPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        // Size clientSize = mapDisplayPictureBox.ClientSize;
        // Size imageSize = mapDisplayPictureBox.Image.Size;
        // mapDisplayMinZoomLevel = (float)clientSize.Width / imageSize.Width;
        // mapDisplayZoomLevel = mapDisplayMinZoomLevel;
        // mapDisplayPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
    }

    private List<string> GetGroundLevels()
    {
        List<string> groundLevels = new()
        {
            "All",
            "M00",
            "M01",
            "M10"
        };
        if (automationModeTabControl.Invoke(()
                => automationModeTabControl.SelectedIndex == 1))
            groundLevels.RemoveAt(0);
        return groundLevels;
    }

    private void PopulateGroundLevels()
    {
        string previousSelectedItem = groundLevelComboBox.SelectedItem?.ToString() ?? "";
        mapConfigurationGroupBox.Enabled = true;
        List<string> groundLevels = GetGroundLevels();
        groundLevelComboBox.Items.Clear();
        List<string> suffixes = new() { "", " (Overworld)", " (Underworld)", " (DLC)" };
        if (automationModeTabControl.SelectedIndex == 1)
            suffixes.RemoveAt(0);
        for (int i = 0; i < groundLevels.Count; i++)
        {
            if (i < suffixes.Count) groundLevels[i] += suffixes[i];
            groundLevelComboBox.Items.Add(groundLevels[i]);
        }

        int index = groundLevelComboBox.Items.IndexOf(previousSelectedItem);
        groundLevelComboBox.SelectedIndex = index != -1 ? index : 0;
    }

    private Dictionary<string, int> GetZoomLevels()
    {
        Dictionary<string, int> dict = new()
        {
            { "All", -1 },
            { "L0", 41 },
            { "L1", 31 },
            { "L2", 11 }
        };
        if (automationModeTabControl.Invoke(()
                => automationModeTabControl.SelectedIndex == 1))
            dict.Remove("All");
        return dict;
    }

    private void PopulateZoomLevels()
    {
        // TODO: Function
        string previousSelectedItem = zoomLevelComboBox.SelectedItem?.ToString() ?? "";
        Dictionary<string, int> zoomLevels = GetZoomLevels();
        zoomLevelComboBox.Items.Clear();
        List<string> zoomLevelKeys = zoomLevels.Keys.ToList();
        List<string> suffixes = new() { "", " (41x41)", " (31x31)", " (11x11)", " (6x6)", " (3x3)" };
        if (automationModeTabControl.SelectedIndex == 1)
            suffixes.RemoveAt(0);
        for (int i = 0; i < zoomLevelKeys.Count; i++)
        {
            if (i < suffixes.Count) zoomLevelKeys[i] += suffixes[i];
            zoomLevelComboBox.Items.Add(zoomLevelKeys[i]);
        }

        int index = zoomLevelComboBox.Items.IndexOf(previousSelectedItem);
        zoomLevelComboBox.SelectedIndex = index != -1 ? index : 0;
    }

    // private void RefreshMapImage(bool resetPosition = false)
    // {
    // if (savedMapImage == null) return;
    // mapDisplayPictureBox.Image?.Dispose();
    // mapDisplayPictureBox.Image = new Bitmap(savedMapImage);
    // mapDisplayPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
    // if (resetPosition) UpdateMapImagePosition(0, 0);
    // mapDisplayMinZoomLevel = -1;
    // }

    private Bitmap? LoadMapImage(string path)
    {
        Bitmap mapImage;
        try
        {
            mapImage = DdsFile.Load(path).CreateAliasedBitmap();
        }
        catch
        {
            return null;
        }

        return new Bitmap(mapImage);
    }

    private void BrowseInputFolderButtonClick(object sender, EventArgs e)
    {
        string[] gameModFolderFiles = GetAllFolderFiles(gameModFolderPath);
        mapTileMaskBndPath = gameModFolderFiles.FirstOrDefault(i => i.Contains(".mtmskbnd.dcx")) ?? "";
        mapTileTpfBhdPath = gameModFolderFiles.FirstOrDefault(i => i.Contains("71_maptile.tpfbhd")) ?? "";
        mapTileTpfBtdPath = mapTileTpfBhdPath.Replace(".tpfbhd", ".tpfbdt");
        if (!ResourceExists(mapTileMaskBndPath, "71_maptile.mtmskbnd.dcx")) return;
        if (!ResourceExists(mapTileTpfBhdPath, "71_maptile.tpfbhd")) return;
        if (!ResourceExists(mapTileTpfBtdPath, "71_maptile.tpfbdt")) return;
        mapTileMaskBnd = BND4.Read(mapTileMaskBndPath);
        mapTileTpfBhd = BXF4.Read(mapTileTpfBhdPath, mapTileTpfBtdPath);

        FolderBrowserDialog dialog = new()
        {
            Description = @"Open Game/Mod Folder",
            UseDescriptionForTitle = true
        };
        if (dialog.ShowDialog() != DialogResult.OK) return;
        inputFolderPath = dialog.SelectedPath;
        automateButton.Enabled = true;
        inputFolderPathLabel.Text = inputFolderPath;

        outputFolderGroupBox.Enabled = true;
        InputFolderGroupBox.Enabled = true;

        // mapDisplayOpenMapImageLabel.Visible = false;
        // mapDisplayGroupBox.Enabled = true;
    }

    // private void MapDisplayPictureBox_MouseDown(object sender, MouseEventArgs e)
    // {
    // if (e.Button != MouseButtons.Left) return;
    // isDraggingMapDisplay = true;
    // mapDisplayXPos = e.X;
    // mapDisplayYPos = e.Y;
    // }

    // private void UpdateMapImagePosition(int x, int y)
    // {
    // int newTop = y + mapDisplayPictureBox.Top - mapDisplayYPos;
    // int newLeft = x + mapDisplayPictureBox.Left - mapDisplayXPos;
    // int imageTop = newTop + mapDisplayPictureBox.Height;
    // int imageLeft = newLeft + mapDisplayPictureBox.Width;
    // if (imageTop > mapDisplayPictureBox.Height)
    //     newTop = 0;
    // if (imageLeft > mapDisplayPictureBox.Width)
    //     newLeft = 0;
    // if (newTop < mapDisplayPictureBox.Parent!.ClientSize.Height - mapDisplayPictureBox.Height)
    //     newTop = mapDisplayPictureBox.Parent.ClientSize.Height - mapDisplayPictureBox.Height;
    // if (newLeft < mapDisplayPictureBox.Parent.ClientSize.Width - mapDisplayPictureBox.Width)
    //     newLeft = mapDisplayPictureBox.Parent.ClientSize.Width - mapDisplayPictureBox.Width;
    // mapDisplayPictureBox.Top = newTop;
    // mapDisplayPictureBox.Left = newLeft;
    // }

    // private void MapDisplayPictureBox_MouseMove(object sender, MouseEventArgs e)
    // {
    // if (!isDraggingMapDisplay) return;
    // UpdateMapImagePosition(e.X, e.Y);
    // }

    // private void MapDisplayPictureBox_MouseUp(object sender, MouseEventArgs e)
    // {
    // isDraggingMapDisplay = false;
    // }

    private void MapDisplayPictureBox_MouseWheel(object? sender, MouseEventArgs e)
    {
        // if (mapDisplayMinZoomLevel < 0) UpdateMapDisplayMinZoomLevel();
        // int oldWidth = mapDisplayPictureBox.Image.Width;
        // int oldHeight = mapDisplayPictureBox.Image.Height;
        // int oldX = mapDisplayPictureBox.Left;
        // int oldY = mapDisplayPictureBox.Top;
        // mapDisplayZoomLevel = e.Delta > 0
        //     ? Math.Min(mapDisplayZoomLevel * (1 + mapDisplayZoomIncrement), mapDisplayMaxZoomLevel)
        //     : Math.Max(mapDisplayZoomLevel * (1 - mapDisplayZoomIncrement), mapDisplayMinZoomLevel);
        // Bitmap mapImage = new(savedMapImage,
        //     new Size((int)(savedMapImage.Width * mapDisplayZoomLevel),
        //         (int)(savedMapImage.Height * mapDisplayZoomLevel)));
        // mapDisplayPictureBox.Image?.Dispose();
        // mapDisplayPictureBox.Image = mapImage;
        // float mouseX = e.X;
        // float mouseY = e.Y;
        // float scaleFactorX = (float)mapImage.Width / oldWidth;
        // float scaleFactorY = (float)mapImage.Height / oldHeight;
        // int newLeft = (int)(oldX - mouseX * (scaleFactorX - 1));
        // int newTop = (int)(oldY - mouseY * (scaleFactorY - 1));
        // mapDisplayPictureBox.Left = newLeft;
        // mapDisplayPictureBox.Top = newTop;
        // int imageTop = newTop + mapDisplayPictureBox.Height;
        // int imageLeft = newLeft + mapDisplayPictureBox.Width;
        // if (imageTop > mapDisplayPictureBox.Height)
        //     newTop = 0;
        // if (imageLeft > mapDisplayPictureBox.Width)
        //     newLeft = 0;
        // if (newTop < mapDisplayPictureBox.Parent!.ClientSize.Height - mapDisplayPictureBox.Height)
        //     newTop = mapDisplayPictureBox.Parent.ClientSize.Height - mapDisplayPictureBox.Height;
        // if (newLeft < mapDisplayPictureBox.Parent.ClientSize.Width - mapDisplayPictureBox.Width)
        //     newLeft = mapDisplayPictureBox.Parent.ClientSize.Width - mapDisplayPictureBox.Width;
        // mapDisplayPictureBox.Top = newTop;
        // mapDisplayPictureBox.Left = newLeft;
    }

    private static byte[] ConvertMagickImageToDDS(IMagickImage image)
    {
        MemoryStream ogDdsStream = new();
        image.Write(ogDdsStream, MagickFormat.Dds);
        Surface ddsSurface = DdsFile.Load(ogDdsStream.ToArray());
        MemoryStream recomDdsStream = new();
        DdsFile.Save(recomDdsStream, DdsFileFormat.BC7, DdsErrorMetric.Perceptual, BC7CompressionSpeed.Fast,
            false, false, ResamplingAlgorithm.Bicubic, ddsSurface, null);
        return recomDdsStream.ToArray();
    }

    private void AutomationModeTabControl_SelectedIndexChanged(object sender, EventArgs e)
    {
        // RefreshMapImage();
        if (groundLevelComboBox.Items.Count <= 0 || zoomLevelComboBox.Items.Count <= 0) return;
        PopulateGroundLevels();
        PopulateZoomLevels();
    }

    private class Matrix
    {
        private readonly Dictionary<string, bool> Data = new();

        public bool this[int zoomLevel, int x, int y]
        {
            get
            {
                string key = GetKey(zoomLevel, x, y);
                return Data.ContainsKey(key) ? Data[key] : false;
            }
            set
            {
                string key = GetKey(zoomLevel, x, y);
                Data[key] = value;
            }
        }

        private static string GetKey(int zoomLevel, int x, int y)
        {
            return string.Join(",", [zoomLevel, x, y]);
        }
    }
}