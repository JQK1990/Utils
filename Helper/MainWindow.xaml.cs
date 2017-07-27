using System;
using System.Data;
using System.Windows;
using System.Windows.Forms;
using Utils.ExcelManager;
using MessageBox = System.Windows.MessageBox;

namespace Helper
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            InitialTray();
            _wsl = WindowState;
        }
        
        private readonly WindowState _wsl;
        private NotifyIcon _notifyIcon;

        private void InitialTray()
        {

            //设置托盘的各个属性
            _notifyIcon = new NotifyIcon
            {
                BalloonTipText = @"程序开始运行",
                Text = @"托盘图标",
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath),
                Visible = true
            };
            _notifyIcon.ShowBalloonTip(1000);
            _notifyIcon.MouseClick += notifyIcon_MouseClick;

            //设置菜单项
            var open = new MenuItem(@"显示");
            open.Click += open_Click;
            //退出菜单项
            var exit = new MenuItem(@"退出");
            exit.Click += exit_Click;

            //关联托盘控件
            MenuItem[] childen = {open, exit };
            _notifyIcon.ContextMenu = new ContextMenu(childen);

            //Utils.DateManager.CnDate date = Utils.DateManager.ChinaDate.GetChinaDate(DateTime.Now);
            //LblDate.Content = date.GetDateString();

            //窗体状态改变时候触发
            StateChanged += SysTray_StateChanged;
        }
        

        private void SysTray_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Visibility = Visibility.Hidden;
            }
        }
        private void open_Click(object sender, EventArgs e)
        {
            Visibility = Visibility.Visible;
            Show();
            WindowState = _wsl;
        }
        

        private void exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"确定要关闭吗?",
                                               @"退出",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Question,
                                                MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                _notifyIcon.Dispose();
                System.Windows.Application.Current.Shutdown();
            }
        }

        
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (Visibility == Visibility.Visible)
            {
                //Visibility = Visibility.Hidden;
            }
            else
            {
                Visibility = Visibility.Visible;
                Show();
                WindowState = _wsl;
            }
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            if (_notifyIcon != null)
            {
                _notifyIcon.Dispose();
            }
        }


        private void BtnImport_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog fd = new OpenFileDialog
                {
                    Filter = @"xls files|*.xlsx|xlsx files (*.xlsx)|*.xlsx",
                    RestoreDirectory = true
                };
                fd.ShowDialog();
                var fileName = fd.FileName;
                if (string.IsNullOrEmpty(fileName))
                    return;
                if (!Utils.FileManager.FileHelper.FileExist(fileName))
                {
                    MessageBox.Show(@"找不到文件，请重新选择！");
                }
                var excelHelper = new ExcelHelper(fileName);
                var dt = excelHelper.ExcelToDataTable("Sheet1", true);
                DataGrid.ItemsSource = dt.DefaultView;
            }
            catch (Exception)
            {
                //Console.WriteLine(exception);
                //throw;
                //ignor
            }
            
        }

        private void BtnExport_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataGrid.ItemsSource == null)
                {
                    MessageBox.Show(@"列表无数据，请导入数据！");
                    return;
                }
                var dt = ((DataView)DataGrid.ItemsSource).Table;
                var sfd = new SaveFileDialog
                {
                    Filter = @"xls files|*.xlsx|xlsx files (*.xlsx)|*.xlsx",
                    RestoreDirectory = true
                };
                sfd.ShowDialog();
                var fileName = sfd.FileName;
                if (string.IsNullOrEmpty(fileName))
                    return;
                var excelHelper = new ExcelHelper(fileName);
                var result = excelHelper.DataTableToExcel(dt, "Sheet1", true);
                if (result > 0)
                {
                    MessageBox.Show(string.Format("成功导出 {0} 条数据。", result - 1));
                }
            }
            catch (Exception)
            {
                //ignor
            }
        }

        private void BtnClear_OnClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(@"是否清除表格内容？", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DataGrid.ItemsSource = null;
            }
        }
    }
}
