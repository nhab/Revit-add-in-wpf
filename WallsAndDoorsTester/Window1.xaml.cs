
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using WallsAndDoorsTester;

namespace ClassLibrary1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Autodesk.Revit.DB.Document Doc;
        ExternalEventDeleteHandler _externaleventDelete;
        ExternalEvent _DeleteExternalevent;
        public ObservableCollection<ElementsInDataGrid> Cols { get; set; }

            
        public Window1(Autodesk.Revit.DB.Document doc, 
            ExternalEventDeleteHandler eh
            , ExternalEvent deleteEvent)
        {
            InitializeComponent();
            Doc = doc;
            _externaleventDelete = eh;
            _DeleteExternalevent = deleteEvent;

        }

        private void CmbSelect_ItemChanged(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Controls.ComboBox comboBox = sender as System.Windows.Controls.ComboBox;
                ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;
                string selection          = selectedItem.Content.ToString().ToLower();
                
                Cols = new ObservableCollection<ElementsInDataGrid>();
                if (selection == "wall" || selection == "all")
                {
                    ICollection<Element> walls = new FilteredElementCollector(Doc, Doc.ActiveView.Id)
                    .OfCategory(BuiltInCategory.OST_Walls).ToElements();

                    foreach (Element wall in walls)
                    {
                        string id = wall.Id.ToString();
                        Cols.Add(new ElementsInDataGrid("Wall", id));
                    }
                }
                if (selection == "door" || selection == "all")
                {
                    ICollection<Element> doors = new FilteredElementCollector(Doc, Doc.ActiveView.Id)
                      .OfCategory(BuiltInCategory.OST_Doors).ToElements();

                    foreach (Element door in doors)
                    {
                        string id = door.Id.ToString();
                        Cols.Add(new ElementsInDataGrid("Door", id));
                    }
                }
                ElementsDataGrid.ItemsSource = Cols;
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
                ElementId elementId = new ElementId(id); // Replace 1234 with the ID of the element you want to select
                Element element = Doc.GetElement(elementId);
                if (element != null)
                {
                    UIDocument uidoc = new UIDocument(Doc);
                  
                    uidoc.Selection.SetElementIds(new List<ElementId> { elementId });
                    uidoc.ShowElements(new List<ElementId> { elementId });
                }
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
                _externaleventDelete._elementId = elementId;
              
                 _DeleteExternalevent.Raise();
            } catch (Exception ex)
            {
                TaskDialog.Show("Revit", ex.Message);
            }

        }

    }
}
