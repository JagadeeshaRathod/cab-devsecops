using iText.Commons.Actions;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Properties;
using IEventHandler = iText.Kernel.Events.IEventHandler;

public class MyEventHandler : IEventHandler
{
    public void HandleEvent(Event @event)
    {
       
        PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
        PdfDocument pdfDoc = docEvent.GetDocument();
        PdfPage page = docEvent.GetPage();
        int pageNumber = pdfDoc.GetPageNumber(page);
        Rectangle pageSize = page.GetPageSize();
        PdfCanvas pdfCanvas = new PdfCanvas(
            page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
        Color limeColor = new DeviceCmyk(0.208f, 0, 0.584f, 0);
        Color blueColor = new DeviceCmyk(0.445f, 0.0546f, 0, 0.0667f);
        pdfCanvas.SaveState()
                .SetFillColor(pdfDoc.GetNumberOfPages() % 2 == 1 ? limeColor : blueColor)
                .Rectangle(pageSize.GetLeft(), pageSize.GetBottom(),
                    pageSize.GetWidth(), pageSize.GetHeight())
                .Fill().RestoreState();
        //Add header and footer
        pdfCanvas.BeginText()
                .SetFontAndSize(PdfFontFactory.CreateFont(StandardFontFamilies.HELVETICA), 7)
                .MoveText(pageSize.GetWidth() / 2 - 60, pageSize.GetTop() - 20)
                .ShowText("THE TRUTH IS OUT THERE")
                .MoveText(60, -pageSize.GetTop() + 30)
                //.ShowText(String.ValueOf(pageNumber))
                .EndText();
        //Add watermark
        Canvas canvas = new Canvas(pdfCanvas, page.GetPageSize());
        canvas.SetFontColor(ColorConstants.WHITE);
        canvas.SetFontSize(60);
        //canvas.SetProperty(Property.FONT_SIZE, 60);
        canvas.SetFont(PdfFontFactory.CreateFont(StandardFontFamilies.HELVETICA));
        //canvas.SetProperty(Property.FONT, PdfFontFactory.CreateFont(StandardFontFamilies.HELVETICA));
        canvas.ShowTextAligned(new iText.Layout.Element.Paragraph("CONFIDENTIAL"),
            298, 421, pdfDoc.GetPageNumber(page),
            TextAlignment.CENTER, VerticalAlignment.MIDDLE, 45);
        pdfCanvas.Release();
    }
}

