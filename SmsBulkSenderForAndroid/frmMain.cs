namespace SmsBulkSenderForAndroid
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Web;
    using System.Windows.Forms;
    using Timer = System.Threading.Timer;

    public partial class frmMain : Form
    {
        private string username = "icer";
        private string password = "icericer";
        private Timer timer;
        private string host = "http://192.168.1.127:25118";

        public frmMain()
        {
            InitializeComponent();
        }

        private void timerRefreshConnectionStatus(object state)
        {
            this.Invoke((Action)(() => this.lblConnectionStatus.Text = "Trying To Connect"));
            bool isConnected = false;
            try
            {
                var page = GetPage(host + "/newmessage?");
                isConnected = page.Contains(@"<form name=""sendform"" method=""GET"" action=""/newmessage"">");
            }
            catch (TimeoutException)
            {
                isConnected = false;
            }

            if (isConnected)
            {
                this.Invoke((Action)(() => SetConnected()));
            }
            else
            {
                this.Invoke((Action)(() => SetDisconnected()));
            }
        }

        private void SetConnected()
        {
            this.lblConnectionStatus.Text = "Connected";
            this.btnImport.Enabled = true;
            this.btnSend.Enabled = true;
        }

        private void SetDisconnected()
        {
            this.lblConnectionStatus.Text = "Disconnected";
            this.btnImport.Enabled = false;
            this.btnSend.Enabled = false;
        }

        private string GetPage(string url)
        {
            using (WebClient client = new WebClient())
            {
                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
                client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
                client.Encoding = Encoding.UTF8;
                try
                {
                    string page = client.DownloadString(url);
                    return page;
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        throw new TimeoutException();
                    }

                    WebResponse resp = ex.Response;
                    using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                    {
                        var r = sr.ReadToEnd();
                        throw new Exception(r, ex);
                    }
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog()
            {
                DefaultExt = ".txt",
                Filter = "(Tab delimit text)*.txt|*.txt",
            };

            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            var inputFile = dlg.FileName;

            if (string.IsNullOrEmpty(inputFile)) return;

            lstSms.Items.Clear();
            this.btnImport.Enabled = false;
            this.btnImport.Text = "Importing";
            Application.DoEvents();
            try
            {
                var cpage = this.GetPage(host + "/addressbook?");
                var regex = new Regex(@"addNumber\('(?<name>.*)','(?<number>\+?\d{4,18})'\)");
                var contacts = regex.Matches(cpage)
                    .OfType<Match>()
                    .Select(m => new { name = m.Groups["name"].Value, number = m.Groups["number"].Value })
                    .GroupBy(o => o.number)
                    .Select(g => g.First())
                    .ToDictionary(o => o.number, o => o.name);

                var ins = ReadAllLines(inputFile)
                    .Select(ln => ln.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries))
                    .Where(s => s.Length == 2)
                    .Select(s => new { address = s[0], body = s[1] })
                    .ToArray();

                this.lstSms.Items.AddRange(ins
                    .Select(s =>
                    {
                        var lvt = new ListViewItem(s.address);
                        var name = contacts.ContainsKey(s.address) ? contacts[s.address] : string.Empty;
                        lvt.SubItems.Add(name);
                        lvt.SubItems.Add(s.body);
                        lvt.SubItems.Add(string.Empty);
                        return lvt;
                    })
                    .ToArray());
            }
            finally
            {
                this.btnImport.Enabled = true;
                this.btnImport.Text = "Import";
            }
        }

        private static IEnumerable<string> ReadAllLines(string filename)
        {
            using (var fs = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs, Encoding.Default, true))
            {
                while (!sr.EndOfStream)
                {
                    yield return sr.ReadLine();
                }
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            timer = new Timer(timerRefreshConnectionStatus, null, 500, 10000);
            this.SetDisconnected();
            ResizeList();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var msg = string.Format(@"{0} message(s) to send, are you sure?", this.lstSms.Items.Count);

            if (MessageBox.Show(msg, "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            var items = this.lstSms.Items
                .OfType<ListViewItem>()
                .Select(lvt => new
                {
                    number = lvt.Text.Trim(),
                    content = lvt.SubItems[2].Text.Trim(),
                    reference = lvt,
                });
            foreach (var item in items)
            {
                var url = string.Format(
                    host + "/newmessage?empf={0}&type=send&text={1}",
                    item.number,
                    HttpUtility.UrlEncode(item.content, Encoding.UTF8));
                var response = this.GetPage(url);
                item.reference.SubItems[3].Text = "Sent";
                Thread.Sleep(100);
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            ResizeList();
        }

        private void ResizeList()
        {
            var otherWidth = this.lstSms.Columns.OfType<ColumnHeader>().Sum(h => h.Width) - colheadContent.Width;
            var scrollbarWidth = 40;
            colheadContent.Width = this.lstSms.Width - otherWidth - scrollbarWidth;
        }
    }
}