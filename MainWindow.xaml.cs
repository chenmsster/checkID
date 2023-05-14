using IDCardNo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace CheckID
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            rtbIDSource.Document = new FlowDocument(new Paragraph(new Run("输入身份证号，一行一个。")));
            rtbIDDescShow.Document = new FlowDocument(new Paragraph(new Run("")));
        }

        private string[] getText(RichTextBox rtb)
        {
            TextRange documentTextRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            char[] separatingStrings = { '\r', '\n' };

            string[] list = documentTextRange.Text.Split(separatingStrings, StringSplitOptions.RemoveEmptyEntries);


            return list;
        }

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            List<String> list = getText(rtbIDSource).ToList<string>();
            StringBuilder desc = new StringBuilder();

            foreach (var item in list)
            {
                string cardNo = item.Trim().ToUpper();
                IDCardInfo cardInfo = null;
                try
                {
                    cardInfo = IDCardHelper.Parse(cardNo);
                    
                    desc.AppendLine(cardNo + "  \t "
                        + cardInfo.GenderDesc + " \t "
                        + cardInfo.Birthday.ToString("yyyy年MM月dd日") + "\t "
                        + cardInfo.Province + cardInfo.Prefecture + cardInfo.County
                        );
                }
                catch (Exception ex)
                {
                    desc.AppendLine(cardNo + "\t " + ex.Message.ToString().Replace("\r", "").Replace("\n", "").Trim());
                }
            }


            rtbIDDescShow.Document = new FlowDocument(new Paragraph(new Run(desc.ToString())));
        }
    }
}
