using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ClassLibrary1;
using System.Windows;
using System.Windows.Controls;

namespace WallsAndDoorsTester
{
    [Transaction(TransactionMode.Manual)]
    public class MyCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
         
            UIApplication uiApp = commandData.Application;
            Document doc = uiApp.ActiveUIDocument.Document;
            ExternalEventDeleteHandler eh = new ExternalEventDeleteHandler();
       
            ExternalEvent _myExternalevent = ExternalEvent.Create(eh);

            Window1 win2 = new Window1(doc,eh,_myExternalevent);
            win2.Show();
           
            return Result.Succeeded;
        }

    }
}
