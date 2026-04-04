using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace WPFDevelopers.Controls
{
    public partial class FilterPopup : WDUserControl
    {
        public IEnumerable<object> Values { get; set; }

        public List<object> SelectedValues { get; private set; }

        public FilterPopup()
        {
            InitializeComponent();
            
        }

        public FilterPopup(IEnumerable<object> values,HashSet<object> selectedValues)
        {

            var items = values
                .Select(v => new FilterItem
                {
                    Value = v,
                    IsChecked =
                        selectedValues == null
                        ? true
                        : selectedValues.Contains(v)
                })
                .ToList();

            list.ItemsSource = items;
        }

        void ApplyClick(object sender, RoutedEventArgs e)
        {
            SelectedValues = list.Items
                .Cast<FilterItem>()
                .Where(i => i.IsChecked)
                .Select(i => i.Value)
                .ToList();

        }
    }
    
}
