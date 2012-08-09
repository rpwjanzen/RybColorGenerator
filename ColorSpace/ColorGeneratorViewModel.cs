using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;

namespace ColorSpace
{
    public sealed class ColorGeneratorViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<RybColor> m_colors;
        public IEnumerable Colors
        {
            get { return m_colors; }
        }

        private readonly RelayCommand m_generateColorsCommand;
        public ICommand GenerateColorsCommand
        {
            get { return m_generateColorsCommand; }
        }

        private int m_count;
        public double Count
        {
            get { return m_count; }
            set
            {
                if (m_count == value)
                    return;

                m_count = (int)value;
                PropertyChanged(this, new PropertyChangedEventArgs("Count"));
                CountText = value.ToString();

                m_generateColorsCommand.Execute(m_count);
            }
        }

        private string m_countText;
        public string CountText
        {
            get { return m_countText; }
            set
            {
                if (m_countText == value)
                    return;

                m_countText = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CountText"));
            }
        }

        public ColorGeneratorViewModel()
        {
            m_colors = new ObservableCollection<RybColor>();

            m_generateColorsCommand = new RelayCommand(parameter =>
                {
                    string text = CountText;
                    if (text == null)
                        return;
                    
                    int count;
                    if (int.TryParse(text, out count))
                    {
                        var colorsGenerator = new ColorGenerator(count);
                        m_colors.Clear();

                        for (int i = 0; i < count; i++)
                        {
                            var color = colorsGenerator.PickNextColor();
                            m_colors.Add(color);
                        }
                    }
                });

            CountText = "15";
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
