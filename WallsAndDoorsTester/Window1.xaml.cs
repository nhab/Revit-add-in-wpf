
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using WallsAndDoorsTester;
using WallsAndDoorsTester.ExternalEvents;

namespace ClassLibrary1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Document Doc;
        ExternalEventDeleteHandler _externalEventHandlerDelete;
        ExternalEvent _DeleteExternalevent;

        ExternalEventHandlerSelect _externalEventHaandlerSelect;
        ExternalEvent _SelectExternalevent;
        string selectedItemText = "all";

        public ObservableCollection<ElementsInDataGrid> data { get; set; }
        

        public Window1(Document doc
            , ExternalEventDeleteHandler deeh
            , ExternalEvent deleteEvent
             , ExternalEventHandlerSelect seeh
            , ExternalEvent selectEvent)
        {
            InitializeComponent();

            Doc = doc;
            
            _externalEventHandlerDelete = deeh;
            _DeleteExternalevent = deleteEvent;

            _externalEventHaandlerSelect = seeh;
            _SelectExternalevent = selectEvent;
        }

        private void RefreshDataGridData( )
        {
            data = new ObservableCollection<ElementsInDataGrid>();
            if (selectedItemText == "wall" || selectedItemText == "all")
            {
                ICollection<Element> walls = new FilteredElementCollector(Doc, Doc.ActiveView.Id)
                .OfCategory(BuiltInCategory.OST_Walls).ToElements();

                foreach (Element wall in walls)
                {
                    string id = wall.Id.ToString();
                    data.Add(new ElementsInDataGrid("Wall", id));
                }
            }
            if (selectedItemText == "door" || selectedItemText == "all")
            {
                ICollection<Element> doors = new FilteredElementCollector(Doc, Doc.ActiveView.Id)
                  .OfCategory(BuiltInCategory.OST_Doors).ToElements();

                foreach (Element door in doors)
                {
                    string id = door.Id.ToString();
                    data.Add(new ElementsInDataGrid("Door", id));
                }
            }
            ElementsDataGrid.ItemsSource = data;
        }

        private void CmbSelect_ItemChanged(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Controls.ComboBox comboBox = sender as System.Windows.Controls.ComboBox;
                ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;
                 selectedItemText          = selectedItem.Content.ToString().ToLower();
                RefreshDataGridData();    
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());

            }
        }
    
        private void SearcheButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                string IdString = button.Tag.ToString();
               
                int id = int.Parse(IdString);
                ElementId elementId = new ElementId(id);
                _externalEventHaandlerSelect._elementId = elementId;
                _SelectExternalevent.Raise();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = sender as Button;
                string IdString = button.Tag.ToString();

                int id = int.Parse(IdString);
                ElementId elementId = new ElementId(id);
                _externalEventHandlerDelete._elementId = elementId;
              
                 _DeleteExternalevent.Raise();
                RefreshDataGridData();
            } catch (Exception ex)
            {
                TaskDialog.Show("Revit", ex.Message);
            }

        }

    }
}
