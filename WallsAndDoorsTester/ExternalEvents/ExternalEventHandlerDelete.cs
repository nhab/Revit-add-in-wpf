using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WallsAndDoorsTester
{
    public class ExternalEventDeleteHandler: IExternalEventHandler
    {
        private Document _doc1;
        public ElementId _elementId { get; set; }

        public ExternalEventDeleteHandler()
        {

        }

        public void Execute(UIApplication uiapp)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            if (null == uidoc ||_elementId==null)
            {
                return; // no document, nothing to do
            }
            Document doc = uidoc.Document;
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("MyEvent");
                uiapp.ActiveUIDocument.Document.Delete(_elementId);
                tx.Commit();
            }
            
        }
        public string GetName()
        {
            return "delete event Handler";
        }
    }
}
