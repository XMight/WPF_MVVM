using System;
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

namespace XMightUICommon
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class DepPropertyExampleUserControl : UserControlWithNotify
    {
        public static DependencyProperty SomeStringProperty = DependencyProperty.Register("SomeString", typeof(String), 
            typeof(DepPropertyExampleUserControl), new FrameworkPropertyMetadata("Some String Prop default value", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static DependencyProperty SomeAttachedStringProperty = DependencyProperty.RegisterAttached("SomeAttachedString", typeof(String), 
            typeof(DepPropertyExampleUserControl), new FrameworkPropertyMetadata("Some Attached String Prop default value", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public String SomeString
        {
            get
            {
                return (String)GetValue(SomeStringProperty);
            }

            set
            {
                SetValue(SomeStringProperty, value);
            }
        }

        public String SomeAttachedString
        {
            get
            {
                return (String)GetValue(SomeAttachedStringProperty);
            }

            set 
            {
                SetValue(SomeAttachedStringProperty, value);
            }
        }

        public DepPropertyExampleUserControl()
        {
            InitializeComponent();
        }
    }
}
