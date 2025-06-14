#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net.NetworkInformation;
using mshtml;
using FSLibrary;
using FSException;
using FSDns;

#endregion

namespace FSWebBrowser
{
    public partial class Browser
    {
        #region Delegates

        public delegate void CanGoBackChangedHandler(object sender, EventArgs e);

        public delegate void CanGoForwardChangedHandler(object sender, EventArgs e);

        public delegate void DocumentCompletedHandler(object sender, WebBrowserDocumentCompletedEventArgs e);

        public delegate void NavigatedHandler(object sender, WebBrowserNavigatedEventArgs e);

        public delegate void NavigatingHandler(object sender, WebBrowserNavigatingEventArgs e);

        public delegate void NewWindowHandler(object sender, CancelEventArgs e);

        public delegate void ProgressChangedHandler(object sender, WebBrowserProgressChangedEventArgs e);

        public delegate void StatusTextChangedHandler(object sender, EventArgs e);

        #endregion

        private readonly List<string> NotValidPages = new List<string>();
        private readonly List<string> ValidPages = new List<string>();

        public Browser()
        {
            BlackList = null;
            AllowNewWindow = true;
            AllowImageExternalLinks = true;
            AllowFileDownload = true;
            AllowIFrames = true;
            CheckBadWords = false;
            BadWords = new string[]
                           {
                               "agilipollao", "alamierda", "bujarra", "bujarrilla", "bujarron", "bujarrón", "cabron", "cabrón", "cabrona", "cabronas", "cabroncete", "cabrones", "cago en", "cagoen", "calentorra", "calentorro", "capulla", "capullas", "cazurra", "cazurro", "cenutrio", "ceporra", "ceporro", "chaqueteros", "chingar", "chocho", "cipote", "cipoton", "cipotón", "cojon", "cojón", "cojones", "cojonudo", "coño", "cretino", "cuesco", "encular", "estupida", "estupido", "folla", "follada", "folladas", "follado", "folladoras", "folladores", "follados", "follamos", "follando", "follao", "follar", "follarse", "follo", "gilipolla", "gilipollas", "gilipoyas", "gilipuertas", "hijadeputa", "hijaputa", "hija puta", "hijasdeputa", "hijasputa", "hijo de puta", "hijo putas", "hijodeputa", "hijoputa", "hijo puta", "hijoputas", "hijos de puta", "hijosdeputa", "hijosputa", "hostia", "idiota", "idiotas", "imbecil", "imbécil", "jilipolla", "jilipollas", "jilipuertas", "joder", "joderos", "joderos", "jodete", "jódete", "jodida", "jodidas", "jodido", "jodidos", "jodienda", "mamon", "mamón", "mamones", "marica", "maricas", "maricon", "maricona", "mariconas", "mariconazo", "maricones", "mentecata", "mentecato", "mierda", "moña", "ostia", "pendeja", "pendejo", "picha", "polla", "pollas", "por el culo", "porro", "pringado", "pringao", "puta", "putas", "puto", "putos", "que os jodan", "ramera", "subnormal", "subnormales", "tarugo", "tortillera", "truño", "zangana", "zopenco", "zurullo"
                           };
            NudeDetect = false;


            InitializeComponent();

            WebBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
            WebBrowser1.ProgressChanged += WebBrowser1_ProgressChanged;
            WebBrowser1.CanGoBackChanged += WebBrowser1_CanGoBackChanged;
            WebBrowser1.CanGoForwardChanged += WebBrowser1_CanGoForwardChanged;
            WebBrowser1.Navigating += WebBrowser1_Navigating;
            WebBrowser1.NewWindow += WebBrowser1_NewWindow;
            WebBrowser1.Navigated += WebBrowser1_Navigated;
            WebBrowser1.StatusTextChanged += WebBrowser1_StatusTextChanged;
        }

        public bool AllowWebBrowserDrop
        {
            get { return WebBrowser1.AllowWebBrowserDrop; }
            set { WebBrowser1.AllowWebBrowserDrop = value; }
        }

        public bool AllowNavigation
        {
            get { return WebBrowser1.AllowNavigation; }
            set { WebBrowser1.AllowNavigation = value; }
        }

        public bool ScriptErrorsSuppressed
        {
            get { return WebBrowser1.ScriptErrorsSuppressed; }
            set { WebBrowser1.ScriptErrorsSuppressed = value; }
        }

        public bool WebBrowserShortcutsEnabled
        {
            get { return WebBrowser1.WebBrowserShortcutsEnabled; }
            set { WebBrowser1.WebBrowserShortcutsEnabled = value; }
        }

        public bool IsWebBrowserContextMenuEnabled
        {
            get { return WebBrowser1.IsWebBrowserContextMenuEnabled; }
            set { WebBrowser1.IsWebBrowserContextMenuEnabled = value; }
        }

        public string DocumentText
        {
            get { return WebBrowser1.DocumentText; }
            set { WebBrowser1.DocumentText = value; }
        }

        public string DocumentTitle
        {
            get { return WebBrowser1.DocumentTitle; }
        }

        public bool CanGoBack
        {
            get { return WebBrowser1.CanGoBack; }
        }

        public bool CanGoForward
        {
            get { return WebBrowser1.CanGoForward; }
        }

        public Uri Url
        {
            get { return WebBrowser1.Url; }
            set { WebBrowser1.Url = value; }
        }

        public List<string> BlackList { get; set; }

        public string StatusText
        {
            get { return WebBrowser1.StatusText; }
        }

        public bool AllowNewWindow { get; set; }

        public bool NudeDetect { get; set; }

        public bool AllowImageExternalLinks { get; set; }

        public bool AllowFileDownload { get; set; }

        public bool AllowIFrames { get; set; }

        public bool CheckBadWords { get; set; }

        public string[] BadWords { get; set; }

        public bool RemoveFlashBanner { get; set; }

        public bool RemoveContextMenu { get; set; }

        public bool CheckIsChildValidPage { get; set; }

        public HtmlDocument Document
        {
            get { return WebBrowser1.Document; }
        }

        public Bitmap CaptureImage(bool thumbnail)
        {
            //bool sbe = WebBrowser1.ScrollBarsEnabled;
            //int w = WebBrowser1.Width;
            //int h = WebBrowser1.Height;

            //WebBrowser1.ScrollBarsEnabled = false;

            if (WebBrowser1.Document != null)
            {
                if (WebBrowser1.Document.Body != null)
                {
                    //WebBrowser1.Width = WebBrowser1.Document.Body.ScrollRectangle.Width;
                    //WebBrowser1.Height = WebBrowser1.Document.Body.ScrollRectangle.Height;
                }
            }

            Bitmap bitmap = new Bitmap(WebBrowser1.Width, WebBrowser1.Height);
            WebBrowser1.DrawToBitmap(bitmap, new Rectangle(0, 0, WebBrowser1.Width, WebBrowser1.Height));

            //restore webbrowser values
            //WebBrowser1.ScrollBarsEnabled = sbe;
            //WebBrowser1.Width = w;
            //WebBrowser1.Height = h;

            if(thumbnail)
            {
                Image imageThumb = bitmap.GetThumbnailImage(200,200,null,new IntPtr());
                bitmap = new Bitmap(imageThumb);
            }

            return bitmap;
        }

        public WebBrowser WebBrowser
        {
            get { return WebBrowser1; }
        }

        public event ProgressChangedHandler ProgressChanged;
        public event NewWindowHandler NewWindow;
        public event CanGoBackChangedHandler CanGoBackChanged;
        public event CanGoForwardChangedHandler CanGoForwardChanged;
        public event NavigatingHandler Navigating;
        public event NavigatedHandler Navigated;
        public event DocumentCompletedHandler DocumentCompleted;
        public event StatusTextChangedHandler StatusTextChanged;

        private void WebBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (Navigated != null) Navigated(sender, e);
        }

        private void WebBrowser1_StatusTextChanged(object sender, EventArgs e)
        {
            if (StatusTextChanged != null) StatusTextChanged(sender, e);
        }

        private void WebBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            if (!AllowNewWindow)
            {
                e.Cancel = true;
            }

            if (NewWindow != null) NewWindow(sender, e);
        }


        public enum EmulMode
        {
            Ie7 = 7000,
            Ie8 = 8000,
            Ie8Forced = 8888,
            Ie9 = 9000,
            Ie9Forced = 9999
        }

        public void EmulDocMode(EmulMode mode)
        {
            string skey = "Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION";
            RegistryKey key;
            string module = System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName;
            try
            {
                key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(skey, true);
                if (key == null)
                {
                    key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(skey);
                }
            }
            catch (Exception)
            {
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(skey);
            }
            if (Convert.ToInt32(key.GetValue(module)) != (int)mode)
            {
                key.SetValue(module, (int)mode, RegistryValueKind.DWord); 
            }
            key.Close();
        }


        public bool IsNetworkLikelyAvailable()
        {
            NetworkInterface[] nis = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface n in nis)
            {
                if(n.OperationalStatus == OperationalStatus.Up)
                {
                    return true;
                }
            }

            return false;
        }

        private void WebBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (!IsNetworkLikelyAvailable())
            {
                MessageBox.Show("Conexión no disponible. Intentalo más tarde.", "Sin conexión",
                                    MessageBoxButtons.OK);
                e.Cancel = true;
            }
            else
            {
                if (CheckIsChildValidPage)
                {
                    //comprobación por lista negra
                    if (BlackList != null)
                    {
                        string url = e.Url.DnsSafeHost.Replace("www.", "");
                        if (BlackList.Contains(url))
                        {
                            e.Cancel = true;
                            WebBrowser1.Stop();
                            MessageBox.Show("[BLACKLIST] La p¨¢gina seleccionada no es válida para niños.", "No válida",
                                            MessageBoxButtons.OK);
                        }
                    }

                    //comprobación por DNS
                    if (!IsValidChildPage(e.Url.DnsSafeHost))
                    {
                        e.Cancel = true;
                        WebBrowser1.Stop();
                        MessageBox.Show("[DNS] La p¨¢gina seleccionada no es válida para niños.", "No válida",
                                        MessageBoxButtons.OK);
                    }
                }

                if (Navigating != null) Navigating(sender, e);
            }
        }

        private void WebBrowser1_CanGoForwardChanged(object sender, EventArgs e)
        {
            if (CanGoForwardChanged != null) CanGoForwardChanged(sender, e);
        }

        private void WebBrowser1_CanGoBackChanged(object sender, EventArgs e)
        {
            if (CanGoBackChanged != null) CanGoBackChanged(sender, e);
        }

        private void WebBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if (ProgressChanged != null) ProgressChanged(sender, e);
        }


        public HtmlElement FindControlByName(string name)
        {
            HtmlElementCollection listOfHtmlControls = WebBrowser1.Document.All;
            foreach (HtmlElement element in listOfHtmlControls)
            {
                if (!(string.IsNullOrEmpty(element.OuterHtml)))
                {
                    if (element.Name == name.Trim())
                    {
                        return element;
                    }
                }
            }
            return null;
        }

        public WebBrowserReadyState ReadyState
        {
            get { return WebBrowser1.ReadyState; }
        }

        public HtmlElement FindControlByTag(string tag)
        {
            HtmlElementCollection listOfHtmlControls = WebBrowser1.Document.All;
            foreach (HtmlElement element in listOfHtmlControls)
            {
                if (!(string.IsNullOrEmpty(element.OuterHtml)))
                {
                    if (element.TagName == tag.Trim())
                    {
                        return element;
                    }
                }
            }
            return null;
        }

        public bool GoBack()
        {
            return WebBrowser1.GoBack();
        }

        public bool GoForward()
        {
            return WebBrowser1.GoForward();
        }

        public void Stop()
        {
            WebBrowser1.Stop();
        }

        public void ShowPageSetupDialog()
        {
            WebBrowser1.ShowPageSetupDialog();
        }

        public void ShowPrintDialog()
        {
            WebBrowser1.ShowPrintDialog();
        }

        public void ShowPrintPreviewDialog()
        {
            WebBrowser1.ShowPrintPreviewDialog();
        }

        public void ShowSaveAsDialog()
        {
            WebBrowser1.ShowSaveAsDialog();
        }

        public void ShowPropertiesDialog()
        {
            WebBrowser1.ShowPropertiesDialog();
        }

        public override void Refresh()
        {
            WebBrowser1.Refresh();
        }

        public bool Navigate(string urlToLoad)
        {
            try
            {
                bool valid = true;
                if (CheckIsChildValidPage)
                {
                    valid = IsValidChildPage(urlToLoad);
                }

                if (valid)
                {
                    WebBrowser1.Navigate(urlToLoad);
                }
                else
                {
                    MessageBox.Show("La p¨¢gina seleccionada no es válida para niños", "No válida", MessageBoxButtons.OK);
                }
            }
            catch (System.Exception ex)
            {
                throw new ExceptionUtil(ex);
            }

            return true;
        }


        public bool ClickButton(HtmlElement btn)
        {
            try
            {
                HTMLInputElement iElement = ((HTMLInputElement) (btn.DomElement));
                iElement.click();
            }
            catch (System.Exception ex)
            {
                throw new ExceptionUtil(ex);
            }

            return true;
        }


        public bool ClickLink(HtmlElement linkToClick)
        {
            try
            {
                HTMLAnchorElement linkElement = ((HTMLAnchorElement) (linkToClick.DomElement));
                linkElement.click();
            }
            catch (System.Exception ex)
            {
                throw new ExceptionUtil(ex);
            }

            return true;
        }


        public bool FillTextBox(HtmlElement element, string valueToFill)
        {
            try
            {
                element.InnerText = valueToFill;
            }
            catch (System.Exception ex)
            {
                throw new ExceptionUtil(ex);
            }

            return true;
        }


        public bool ScreenShot(string baseDir, string pictureName)
        {
            Rectangle rec = new Rectangle();

            try
            {
                rec.Offset(0, 0);
                rec.Size = WebBrowser1.Document.Window.Size;

                Bitmap bmp = new Bitmap(rec.Width, rec.Height);
                WebBrowser1.DrawToBitmap(bmp, rec);

                bmp.Save(Path.Combine(baseDir, pictureName), ImageFormat.Jpeg);

                return true;
            }
            catch (System.Exception ex)
            {
                throw new ExceptionUtil(ex);
            }
        }


        public bool SelectListBox(HtmlElement dropdown, string value)
        {
            try
            {
                HTMLSelectElement iElement = ((HTMLSelectElement) (dropdown.DomElement));
                iElement.value = value;
            }
            catch (System.Exception ex)
            {
                throw new ExceptionUtil(ex);
            }

            return true;
        }


        public bool SelectRadioButton(HtmlElement radioToSelect)
        {
            try
            {
                HTMLInputElement iElement = ((HTMLInputElement) (radioToSelect.DomElement));
                //iElement.checkedIdent = true; 
                iElement.@checked = true;
            }
            catch (System.Exception ex)
            {
				throw new ExceptionUtil(ex);
            }

            return true;
        }


        private void acx_FileDownload(bool ActiveDocument, ref bool Cancel)
        {
            if (!AllowFileDownload)
            {
                Cancel = true;
            }
        }


        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //while (WebBrowser1.ReadyState != WebBrowserReadyState.Complete) { Application.DoEvents(); }

            if (!AllowFileDownload)
            {
                //no permitimos la descarga de ficheros
                SHDocVw.WebBrowser acx = (SHDocVw.WebBrowser)WebBrowser1.ActiveXInstance;
                acx.FileDownload += acx_FileDownload;
            }

            if (!AllowImageExternalLinks)
            {
                RemoveImageExternalLinks();
            }

            if (!AllowIFrames)
            {
                RemoveIframesScript();
            }

            if (RemoveFlashBanner)
            {
                RemoveFlashBannerData();
            }

            if (RemoveContextMenu)
            {
                if (WebBrowser1.Document != null)
                    WebBrowser1.Document.ContextMenuShowing += Document_ContextMenuShowing;
            }

            // comprobamos si la carga del documento es completa realmente
            if (WebBrowser1.Url == e.Url)
            {
  
                //comprobamos las imagenes de desnudos
                if (NudeDetect)
                {
                    CheckNudeImages();
                }

                //comprobamos las palabras no permitidas
                if (CheckBadWords)
                {
                    CheckBadWord();
                }

                if (DocumentCompleted != null) DocumentCompleted(sender, e);
            }
        }

        private void CheckBadWord()
        {
            if (WebBrowser1.Document != null)
                if (WebBrowser1.Document.Body != null)
                {
				string docText = FSLibrary.TextUtil.RemoveExpressionSignals(FSLibrary.TextUtil.RemoveHtmlTags(WebBrowser1.DocumentText));

                    foreach (string badWord in BadWords)
                    {
                        if (docText.Contains(" " + badWord + " "))
                        {
                            RemoveLinks();
                            //WebBrowser1.Document.Body.SetAttribute("Background", "#222222");
                            WebBrowser1.Document.Body.Enabled = false;
                            MessageBox.Show(string.Format("P¨¢gina con contenido no adecuado para menores. Palabra: [{0}]", badWord));
                            break;
                        }
                    }
                }
        }

        private void Document_ContextMenuShowing(object sender, HtmlElementEventArgs e)
        {
            if (RemoveContextMenu)
            {
                e.ReturnValue = false;
            }
        }


        private void RemoveImageExternalLinks()
        {
            //leemos todos los links
            if (WebBrowser1.Document != null)
                foreach (HtmlElement he in WebBrowser1.Document.GetElementsByTagName("a"))
                {
                    HtmlElementCollection imgs = he.GetElementsByTagName("img");

                    //si el link contiene una imagen ... la quitamos (banners, publicidad, etc ...)
                    if (imgs.Count > 0)
                    {
                        string src = he.GetAttribute("href");

                        if (src != "")
                        {
                            //if (!(src.StartsWith("?") || src.StartsWith("/") || src.StartsWith("#")))
                            if (src.ToLower().StartsWith("http"))
                            {
								if (FSNetwork.Net.IsUri(src))
                                {
                                    Uri url = new Uri(src);

                                    //si es un link externo
                                    if (url.Host != WebBrowser1.Url.Host)
                                    {
                                        he.OuterText = ""; //quitamos el contenido completo
                                    }
                                }
                            }
                        }
                    }
                }
        }

        private void RemoveIframesScript()
        {
            //leeemos todos los iframes
            if (WebBrowser1.Document != null)
                foreach (HtmlElement he in WebBrowser1.Document.GetElementsByTagName("iframe"))
                {
                    string src = he.GetAttribute("src");

                    //si tiene src
                    if (src != "")
                    {
                        //if (!(src.StartsWith("?") || src.StartsWith("/") || src.StartsWith("#")))
                        if(src.ToLower().StartsWith("http"))
                        {
                            //si es una Url valida
							if (FSNetwork.Net.IsUri(src))
                            {
                                Uri url = new Uri(src);

                                //si es un src externo
                                if (url.Host != WebBrowser1.Url.Host)
                                {
                                    //lo borramos
                                    he.SetAttribute("src", "");
                                    //he.Document.Write(String.Empty);
                                }
                            }
                        }
                    }
                }
        }


        private void RemoveLinks()
        {
            //leemos todos los links
            if (WebBrowser1.Document != null)
            {
                foreach (HtmlElement he in WebBrowser1.Document.GetElementsByTagName("a"))
                {
                    he.SetAttribute("href", "/");
                }
            }
        }


        [DllImport("urlmon.dll", CharSet = CharSet.Auto, PreserveSig = false)]
        private static extern void URLDownloadToFile(
            [MarshalAs(UnmanagedType.IUnknown)] object pCaller,
            [MarshalAs(UnmanagedType.LPTStr)] string szUrl,
            [MarshalAs(UnmanagedType.LPTStr)] string szFileName,
            Int32 dwReserved,
            IntPtr lpfnCb);

        private void CheckNudeImages()
        {
			FSGraphics.ImageProcess skin = new FSGraphics.ImageProcess();
            Bitmap bm;

            //leemos todos los links
            if (WebBrowser1.Document != null)
            {
                foreach (HtmlElement he in WebBrowser1.Document.GetElementsByTagName("img"))
                {
                    string width = he.GetAttribute("width");
                    string height = he.GetAttribute("height");
                    int w = Convert.ToInt32(width);
                    int h = Convert.ToInt32(height);

                    if(h < 100 || w < 100)continue;

                    string src = he.GetAttribute("src");
                    if (src != "")
                    {
                        if (src.StartsWith("data:"))
                        {
                            bm = new Bitmap(skin.ConvertBase64ToImage(src));
                        }
                        else
                        {
                            string ext = src.Substring(src.Length - 4, 4);

                            //en teoria esta función lee de la cache las imagenes en vez de descargarlo de nuevo
                            URLDownloadToFile(WebBrowser1.ActiveXInstance as SHDocVw.IWebBrowser2, src, "tempImg" + ext, 0, IntPtr.Zero);
                            bm = new Bitmap("tempImg" + ext);
                        }

                        using (Bitmap bmp = bm)
                        {
                            if (bmp.Height > 100 && bmp.Width > 100)
                            {
                                int perc = skin.DetectSkinPercent(bmp);

                                if(perc > 40)
                                {
                                    he.OuterText = "";
                                }
                            }
                        }
                    }
                }
            }
            

            
        }

        private void CheckNudeImages2()
        {
            FSGraphics.ImageProcess skin = new FSGraphics.ImageProcess();

            //leemos todas las imagenes a traves del portapapeles
            if (WebBrowser1.Document != null)
            {
                IHTMLDocument2 doc = (IHTMLDocument2)WebBrowser1.Document.DomDocument;
                IHTMLControlRange imgRange = (IHTMLControlRange)((HTMLBody)doc.body).createControlRange();

                MessageBox.Show("Total imagenes: " + doc.images.length.ToString());

                foreach (IHTMLImgElement img in doc.images)
                {
                    imgRange.add((IHTMLControlElement)img);

                    imgRange.execCommand("Copy", false, null);

                    using (Bitmap bmp = (Bitmap)FSFormLibrary.Clipboard.GetDataObject().GetData(DataFormats.Bitmap))
                    {

                        if (bmp != null)
                        {
                            if (bmp.Height > 100 && bmp.Width > 100)
                            {
                                //bmp.Save(@"C:\" + img.nameProp);
                                int skinPerc = skin.DetectSkinPercent(bmp);

                                if (skinPerc > 30)
                                {
                                    MessageBox.Show("Imagen: " + img.src + "Porcentaje:" + skinPerc.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }

        private void RemoveIFramesScript2()
        {
            try
            {
                //cancelamos la nevagación en IFRAMES
                IHTMLDocument2 doc = CrossFrameIE.GetDocumentFromBrowser(WebBrowser1);
                for (int fo = 0; fo < doc.frames.length; fo++)
                {
                    object objF = fo;
                    IHTMLWindow2 obj = (IHTMLWindow2) doc.frames.item(ref objF);
                    IHTMLDocument2 obj2 = CrossFrameIE.GetDocumentFromWindow(obj);

                    obj2.designMode = "On";
                    obj2.write("");
                    obj2.close();
                    obj2.designMode = "Off";
                }
            }
            catch
            {
            }
        }

        #region "Remove Flash Banner"

        private void RemoveFlashBannerData()
        {
            IntPtr webHandle = WebBrowser1.Handle;

            webHandle = Win32API.FindWindowEx(webHandle, IntPtr.Zero, "Shell Embedding", IntPtr.Zero);
            webHandle = Win32API.FindWindowEx(webHandle, IntPtr.Zero, "Shell DocObject View", IntPtr.Zero);
            webHandle = Win32API.FindWindowEx(webHandle, IntPtr.Zero, "Internet Explorer_Server", IntPtr.Zero);
            webHandle = Win32API.FindWindowEx(webHandle, IntPtr.Zero, "MacromediaFlashPlayerActiveX", IntPtr.Zero);

            if (webHandle != IntPtr.Zero)
            {
                Thread.Sleep(500);
                Win32API.PostMessage(webHandle, Win32APIEnums.WM_MOUSEMOVE, new IntPtr(Win32APIEnums.MOUSEEVENTF_MOVE),
                            MakeLParam(17, 376)); //boton de play
                Win32API.PostMessage(webHandle, Win32APIEnums.WM_LBUTTONDOWN, new IntPtr(Win32APIEnums.MOUSEEVENTF_MOVE),
                            MakeLParam(17, 376));
                Win32API.PostMessage(webHandle, Win32APIEnums.WM_LBUTTONUP, new IntPtr(Win32APIEnums.MOUSEEVENTF_MOVE),
                            MakeLParam(17, 376));
            }
        }

        private static IntPtr MakeLParam(int loWord, int hiWord)
        {
            return (IntPtr) ((hiWord << 16) | (loWord & 0xffff));
        }

        #endregion

        #region "DNS Valid Page"

        private bool IsValidChildPage(string url)
        {
            if (url == "") return true;
            if (NotValidPages.Contains(url)) return false;
            if (ValidPages.Contains(url)) return true;

            Resolver resolver1 = new Resolver();
            resolver1.DnsServer = "208.67.222.123";  //OpenDNS

            Response response = resolver1.Query(url, QType.A, QClass.IN);
            foreach (RR rr in response.Answers)
            {
                if (IsFamilyShield(rr.RECORD.ToString()))
                {
                    if (!NotValidPages.Contains(url))
                        NotValidPages.Add(url);
                    return false;
                }
            }

            if (!ValidPages.Contains(url))
                ValidPages.Add(url);
            return true;
        }

        private bool IsFamilyShield(string ip)
        {
            string[] bad = {
                               "67.215.65.130", "67.215.65.131", "67.215.65.133", "67.215.65.134", "67.215.65.135",
                               "67.215.65.136", "67.215.65.137", "67.215.65.138", "67.215.65.139", "67.215.65.140",
                               "67.215.65.141", "67.215.65.142", "67.215.65.143", "67.215.65.144", "67.215.65.145"
                           };

            foreach (string s in bad)
            {
                if (ip == s) return true;
            }

            return false;
        }

        #endregion
    }
}