using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ClassLibrary1;
using System.Windows;
using System.Windows.Controls;
using WallsAndDoorsTester.ExternalEvents;

namespace WallsAndDoorsTester
{
    [Transaction(TransactionMode.Manual)]
    public class MyCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
         
            UIApplication uiApp = commandData.Application;
            Document doc = uiApp.ActiveUIDocument.Document;
            ExternalEventDeleteHandler deleteExternalEventHandler = new ExternalEventDeleteHandler();
            ExternalEventHandlerSelect selectExternalEventHandler = new ExternalEventHandlerSelect();

            ExternalEvent _myDeleteExternalevent = ExternalEvent.Create(deleteExternalEventHandler);
            ExternalEvent _mySelectExternalEvent = ExternalEvent.Create(selectExternalEventHandler);  
            Window1 win2 = new Window1(
                doc, 
                deleteExternalEventHandler
                , _myDeleteExternalevent
                , selectExternalEventHandler
                ,_mySelectExternalEvent
                );
            win2.Show();
           
            return Result.Succeeded;
        }

    }
}
