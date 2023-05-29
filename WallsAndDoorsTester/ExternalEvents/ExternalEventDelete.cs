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
    /// <summary>
    /// usage:
    ///   MyExternalEventHandler handler = new MyExternalEventHandler();
    /// ExternalEvent externalEvent = ExternalEvent.Create(handler);
    /// externalEvent.Raise();
    /// </summary>
    public class ExternalEventDeleteHandler: IExternalEventHandler
    {
        private Document _doc1;
        public ElementId _elementId { get; set; }

        public ExternalEventDeleteHandler()//Document doc1,ElementId elementId)
        {
            ///_doc1 = doc1;
            ///_elementId = elementId;

            //var transaction =new Transaction(_doc1, "delete transaction");

            //transaction.Start();
            //try
            //{
            //    _doc1.Delete(_elementId);
            //}catch(Exception ex)
            //{
            //    transaction.RollBack();
            //}
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
        /*
        public static ExternalEvent Create(Document doc, ElementId elementId)
        {
            try
            {
                ExternalEvent e = ExternalEvent.Create(new ExternalEventDelete(doc, elementId));

                return e;
            }catch(Exception e)
            {
                string s = e.Message;
                return null;
            }
        }*/
    }
}
