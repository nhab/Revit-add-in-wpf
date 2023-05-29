using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WallsAndDoorsTester.ExternalEvents
{
    public class ExternalEventHandlerSelect: IExternalEventHandler
    {
        private Document _doc1;
        public ElementId _elementId { get; set; }
        
        public void Execute(UIApplication uiapp)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            if (null == uidoc || _elementId == null)
            {
                return; // no document or element, nothing to do
            }
            Document doc = uidoc.Document;
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("MySelectEvent");

                if (_elementId != null)
                {
                    uidoc.Selection.SetElementIds(new List<ElementId> { _elementId });
                    uidoc.ShowElements(new List<ElementId> { _elementId });
                }
                tx.Commit();
            }

        }
        public string GetName()
        {
            return "select and zoom event Handler";
        }

    }
}
