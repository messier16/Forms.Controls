using System;

using Xamarin.Forms;
using Messier16.Forms.Controls;

namespace TestApp.Pages.SegmentedControlPages
{
    public class SegmentedControlCodePage : ContentPage
    {
        Label sportLabel;
        SegmentedControl DisabledSegmentedControl;

        string [] values = { "Artists", "Albums", "Songs", "Local" };

        public SegmentedControlCodePage()
        {

            var segmentedControl = new SegmentedControl();
            segmentedControl.Children.Add(new SegmentedControlOption { Text = "Valor 1" });
            segmentedControl.Children.Add(new SegmentedControlOption { Text = "Valor 2" });
            segmentedControl.Children.Add(new SegmentedControlOption { Text = "Valor 3" });
            segmentedControl.Margin = 10;


            DisabledSegmentedControl = new SegmentedControl();
            foreach (var item in values)
            {
                DisabledSegmentedControl.Children.Add(new SegmentedControlOption { Text = item });
            }
            DisabledSegmentedControl.Margin = 10;

            var picker = new Picker();
            picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
            foreach (var item in values)
            {
                picker.Items.Add(item);
            }
            picker.SelectedIndex = 0;


            sportLabel = new Label { Text = "<- Choose",
                VerticalTextAlignment = TextAlignment.Center ,
                HorizontalTextAlignment = TextAlignment.Center };

            var sportChoose = new SegmentedControl();
            sportChoose.Margin = 10;
            sportChoose.Children.Add(new SegmentedControlOption { Text = "Futbol" });
            sportChoose.Children.Add(new SegmentedControlOption { Text = "Soccer" });
            sportChoose.PropertyChanged += SportChoose_PropertyChanged;
            sportChoose.SelectedIndex = 0;


            var grid = new Grid
            {
                    ColumnSpacing = 0,
                    RowSpacing = 0,
                    RowDefinitions = 
                        {
                            new RowDefinition {Height=new GridLength(1,GridUnitType.Auto)},
                            new RowDefinition {Height=new GridLength(1,GridUnitType.Auto)},
                            new RowDefinition {Height=new GridLength(1,GridUnitType.Auto)},
                            new RowDefinition {Height=new GridLength(1,GridUnitType.Auto)}
                        },
                    ColumnDefinitions = 
                        {
                            new ColumnDefinition(),
                            new ColumnDefinition(),
                            new ColumnDefinition()
                        }
            };
            

            grid.Children.Add(segmentedControl);
            Grid.SetColumnSpan(segmentedControl, 3);

            Grid.SetRow(sportChoose, 1);
            grid.Children.Add(sportChoose);
            Grid.SetColumnSpan(sportChoose, 2);

            Grid.SetRow(sportLabel, 1);
            Grid.SetColumn(sportLabel, 2);
            grid.Children.Add(sportLabel);

            Grid.SetRow(picker, 2);
            grid.Children.Add(picker);
            Grid.SetColumnSpan(picker, 3);

            Grid.SetRow(DisabledSegmentedControl, 3);
            grid.Children.Add(DisabledSegmentedControl);
            Grid.SetColumnSpan(DisabledSegmentedControl, 3);


            Content = grid;
        }

        void Picker_SelectedIndexChanged (object sender, EventArgs e)
        {
            var picker = sender as Picker;
            DisabledSegmentedControl.SelectedIndex = picker.SelectedIndex;
        }

        void SportChoose_PropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var segmentedControl = sender as SegmentedControl;
            if (e.PropertyName.Equals("SelectedIndex"))
            {
                sportLabel.Text = segmentedControl.SelectedText;
            }
        }
    }
}


