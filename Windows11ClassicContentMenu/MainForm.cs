using Serilog;
using Serilog.Events;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace Windows11ClassicContentMenu
{
    public partial class MainForm : Form
    {
        private const string BASE_REG_PATH = @"HKCU\Software\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}";
        private const string APP_NAME = @"Windows11ClassicContentMenu";
        private const string GITHUB_HOME = @"https://github.com/luzilong2005/Window11ClassicContentMenu";
        private const string LICENSE_CONTENT = @"Window11ClassicContentMenu © 2026 by luzilong2005 is licensed under CC BY-NC-SA 4.0";

        private XmlDocument langXmlDocument = new XmlDocument();

        public MainForm()
        {
            InitializeComponent();

            try
            {
                string logDir = GetLogDir();
                string logFileName = Path.Combine(logDir, "Log_.log");

                if (!Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .WriteTo.File(
                        path: logFileName,
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: 30,
                        fileSizeLimitBytes: 10 * 1024 * 1024,
                        encoding: System.Text.Encoding.UTF8,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u}] {Message:lj}{NewLine}{Exception}"
                    )
                    .CreateLogger();

                Log.Information("[Program Start] Log system initialized successfully, log storage path: {LogPath}", logFileName);
                Log.Debug("System information at program startup: OSVersion={OSVersion}, CurrentUser={UserName}",
                    Environment.OSVersion.VersionString, Environment.UserName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize log system: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Logger = new LoggerConfiguration().CreateLogger();
                Log.Fatal(ex, "Log system initialization failed");
            }

            InitApplicationLanguage();
        }

        private void InitApplicationLanguage()
        {
            try
            {
                Log.Debug("Start initializing UI controls");
                LanguageSelector.Items.AddRange(new object[] { "简体中文", "English" });
                LanguageSelector.Text = LanguageSelector.Items[0].ToString();
                LanguageSelector.SelectedIndexChanged += ComboBoxLanguage_SelectedIndexChanged;

                string langCode = System.Globalization.CultureInfo.CurrentCulture.Name;

                Log.Debug($"Load default language configuration: {langCode}");
                LoadLanguageXml(langCode);

                Log.Debug("Update UI text to default language");
                UpdateUIByLanguage();

                Log.Information("[Program Start] UI initialization completed, default language loaded");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred during UI initialization");
                MessageBox.Show($"UI initialization failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetLogDir()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), APP_NAME, "Logs");
        }

        private void RestartExplorer()
        {
            Log.Information("[Operation] Start restarting explorer.exe");
            try
            {
                using (Process p = new Process())
                {
                    p.StartInfo = new ProcessStartInfo
                    {
                        FileName = "taskkill.exe",
                        Arguments = "/f /im explorer.exe",
                        WindowStyle = ProcessWindowStyle.Hidden,
                        UseShellExecute = true,
                        CreateNoWindow = true
                    };
                    Log.Debug("Execute command to terminate explorer.exe: taskkill.exe /f /im explorer.exe");
                    p.Start();

                    if (!p.WaitForExit(10000))
                    {
                        Log.Warning("Timeout (10 seconds) when terminating explorer.exe, force kill process");
                        p.Kill();
                    }
                    else
                    {
                        Log.Information("explorer.exe process terminated successfully, exit code: {ExitCode}", p.ExitCode);
                    }
                }

                using (Process p = new Process())
                {
                    string explorerPath = Path.Combine(Environment.GetEnvironmentVariable("WINDIR"), "explorer.exe");
                    p.StartInfo.FileName = explorerPath;
                    p.StartInfo.UseShellExecute = true;

                    Log.Debug("Restart explorer.exe, path: {ExplorerPath}", explorerPath);
                    p.Start();
                    Log.Information("explorer.exe restarted successfully");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred while restarting explorer.exe");
                MessageBox.Show($"Failed to restart explorer.exe: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLanguageXml(string langCode)
        {
            Log.Debug("[Language Switch] Start loading language configuration from embedded resource, language code: {LangCode}", langCode);
            try
            {
                string resourceName = $"{Assembly.GetExecutingAssembly().GetName().Name}.Languages.{langCode}.xml";
                Log.Debug("Embedded language resource name: {ResourceName}", resourceName);

                using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    if (resourceStream == null)
                    {
                        string errorMsg = $"Embedded language resource not found: {resourceName}";
                        Log.Error(errorMsg);
                        throw new FileNotFoundException(errorMsg, resourceName);
                    }

                    langXmlDocument.Load(resourceStream);
                    Log.Information("[Language Switch] Embedded language resource loaded successfully: {LangCode}", langCode);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Language Switch] Failed to load embedded language resource: {LangCode}", langCode);
                MessageBox.Show($"Failed to load {langCode} language configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetLangText(string textKey)
        {
            try
            {
                XmlNode xmlNode = langXmlDocument.SelectSingleNode($"//Text[@Key='{textKey}']");
                string result = xmlNode?.InnerText ?? textKey;
                Log.Debug("[Language] Get text by key: {TextKey} = {Result}", textKey, result);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Language] Failed to get text by key: {TextKey}", textKey);
                return textKey;
            }
        }

        private void UpdateUIByLanguage()
        {
            Log.Information("[UI Update] Start updating UI text by language");
            try
            {
                Text = GetLangText("Window_Title");
                BtnClassic.Text = GetLangText("Button_ClassicStyle");
                BtnDefault.Text = GetLangText("Button_DefaultStyle");
                BtnRestartExplorer.Text = GetLangText("Button_RestartExplorer");
                TopMenuHelp.Text = GetLangText("TopMenu_Help");
                TopMenuItemAbout.Text = GetLangText("TopMenuItem_About");
                TopMenuItemLogDirectory.Text = GetLangText("TopMenuItem_LogDirectory");
                TopMenuItemLogClear.Text = GetLangText("TopMenuItem_LogClear");

                Log.Information("[UI Update] UI text updated successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[UI Update] Failed to update UI text");
            }
        }

        private void ComboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Log.Debug("[Language Switch] Language selector index changed, selected index: {SelectedIndex}", LanguageSelector.SelectedIndex);
            try
            {
                if (LanguageSelector.SelectedIndex == 0)
                {
                    LoadLanguageXml("zh-CN");
                }
                else if (LanguageSelector.SelectedIndex == 1)
                {
                    LoadLanguageXml("en-US");
                }
                UpdateUIByLanguage();
                Log.Information("[Language Switch] Language switched successfully, selected language: {SelectedText}", LanguageSelector.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Language Switch] Failed to switch language");
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Log.Information("[Program Start] Main form loaded completely");
        }

        private void BtnRestartExplorer_Click(object sender, EventArgs e)
        {
            Log.Information("[User Operation] Click button: {ButtonName}", BtnRestartExplorer.Name);
            RestartExplorer();
        }

        private void BtnClassic_Click(object sender, EventArgs e)
        {
            Log.Information("[User Operation] Click button: {ButtonName}", BtnClassic.Name);

            try
            {
                using (Process p = new Process())
                {
                    p.StartInfo = new ProcessStartInfo
                    {
                        FileName = "reg.exe",
                        Arguments = $"add \"{BASE_REG_PATH}\\InprocServer32\" /f /ve",
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };
                    Log.Debug("[Registry Operation] Execute reg command: {Command}", $"reg.exe add \"{BASE_REG_PATH}\\InprocServer32\" /f /ve");
                    p.Start();
                    var output = p.StandardOutput.ReadToEnd();
                    var error = p.StandardError.ReadToEnd();
                    p.WaitForExit();
                    var exitCode = p.ExitCode;

                    Log.Information("[Registry Operation] Reg command executed, exit code: {ExitCode}, output: {Output}, error: {Error}", exitCode, output, error);

                    if (exitCode == 0)
                    {
                        MessageBox.Show($"{output}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Log.Information("[Registry Operation] Set classic context menu successfully");
                    }
                    else
                    {
                        MessageBox.Show($"ExitCode: {exitCode}\n{output}\n{error}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Log.Warning("[Registry Operation] Failed to set classic context menu, exit code: {ExitCode}", exitCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Registry Operation] Error occurred when setting classic context menu");
                MessageBox.Show($"{ex.Message}\n", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDefault_Click(object sender, EventArgs e)
        {
            Log.Information("[User Operation] Click button: {ButtonName}", BtnDefault.Name);

            try
            {
                using (Process p = new Process())
                {
                    p.StartInfo = new ProcessStartInfo
                    {
                        FileName = "reg.exe",
                        Arguments = $"delete \"{BASE_REG_PATH}\" /f",
                        UseShellExecute = false,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };
                    Log.Debug("[Registry Operation] Execute reg command: {Command}", $"reg.exe delete \"{BASE_REG_PATH}\" /f");
                    p.Start();
                    var output = p.StandardOutput.ReadToEnd();
                    var error = p.StandardError.ReadToEnd();
                    p.WaitForExit();
                    var exitCode = p.ExitCode;

                    Log.Information("[Registry Operation] Reg command executed, exit code: {ExitCode}, output: {Output}, error: {Error}", exitCode, output, error);

                    if (exitCode == 0)
                    {
                        MessageBox.Show($"{output}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Log.Information("[Registry Operation] Restore default context menu successfully");
                    }
                    else
                    {
                        MessageBox.Show($"ExitCode: {exitCode}\n{output}\n{error}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Log.Warning("[Registry Operation] Failed to restore default context menu, exit code: {ExitCode}", exitCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Registry Operation] Error occurred when restoring default context menu");
                MessageBox.Show($"{ex.Message}\n", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            Log.Information("[Program Exit] Main form closed, start releasing log resources");
            Log.CloseAndFlush();
        }

        private void TopMenuItemGithub_Click(object sender, EventArgs e)
        {
            string targetUrl = GITHUB_HOME;
            try
            {
                Process.Start(new ProcessStartInfo 
                { 
                    FileName = targetUrl,
                    UseShellExecute = true
                });
                Log.Information($"Opened the webpage {targetUrl} successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Failed to open the webpage {targetUrl}");
                MessageBox.Show($"{ex.Message}\n", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TopMenuItemLogDirectory_Click(object sender, EventArgs e)
        {
            string logDir = GetLogDir();
            try
            {
                Process.Start(new ProcessStartInfo
                { 
                    FileName = GetLogDir(),
                    UseShellExecute = true
                });
                Log.Information($"Opened the directory {logDir} successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Failed to open the directory {logDir}");
                MessageBox.Show($"{ex.Message}\n", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TopMenuItemLogClear_Click(object sender, EventArgs e)
        {
            string logDir = GetLogDir();
            string[] files = Directory.GetFiles(logDir);
            foreach (string file in files)
            {
                try
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                    Log.Information($"File {file} deleted successfully");
                }
                catch (IOException ex)
                {
                    Log.Error(ex, $"File {file} deleted failed");
                }
            }

            Log.Information("Log cleanup completed successfully");
            MessageBox.Show("Info", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TopMenuItemAbout_Click(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string appName = APP_NAME;
            string version = assembly.GetName().Version.ToString(3);
            string arch = RuntimeInformation.ProcessArchitecture.ToString();
            
            MessageBox.Show($"{appName}\n{version} - {arch}\n\n{LICENSE_CONTENT}\n", GetLangText("TopMenuItem_About"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}