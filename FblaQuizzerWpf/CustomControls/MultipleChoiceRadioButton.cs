using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FblaQuizzerWpf.CustomControls
{
    public class MultipleChoiceRadioButton : RadioButton
    {
        public static readonly DependencyProperty MultipleChoiceOptionProperty = DependencyProperty
            .Register("MultipleChoiceOption", typeof(Guid), typeof(MultipleChoiceRadioButton), new PropertyMetadata(null));
        
        public Guid MultipleChoiceOption
        {
            get
            {
                return (Guid)this.GetValue(MultipleChoiceOptionProperty);
            }

            set
            {
                this.SetValue(MultipleChoiceOptionProperty, value);
            }
        }

        // https://stackoverflow.com/questions/1317891/simple-wpf-radiobutton-binding
        public object RadioBinding
        {
            get { return (object)GetValue(RadioBindingProperty); }
            set { SetValue(RadioBindingProperty, value); }
        }

        public static readonly DependencyProperty RadioBindingProperty =
        DependencyProperty.Register(
            "RadioBinding",
            typeof(object),
            typeof(MultipleChoiceRadioButton),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnRadioBindingChanged));

        private static void OnRadioBindingChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
        {
            MultipleChoiceRadioButton rb = (MultipleChoiceRadioButton)d;
            if (rb.MultipleChoiceOption.Equals(e.NewValue))
                rb.SetCurrentValue(RadioButton.IsCheckedProperty, true);
        }

        protected override void OnChecked(RoutedEventArgs e)
        {
            base.OnChecked(e);
            SetCurrentValue(RadioBindingProperty, MultipleChoiceOption);
        }
    }

}
