using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Domain.Models; // Assuming this namespace contains UnvalidatedProduct
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.EntityFrameworkCore;
using static Domain.Models.ShoppingEvent;

namespace Domain.Workflow
{
    public class CreateBillWorkFlow
    {
        private readonly ProiectPsscDb dbContext;

        public CreateBillWorkFlow(ProiectPsscDb dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IShoppingEvent> GeneratePdfForOrder(string orderHeaderId)
        {
            IShoppingEvent shoppingEvent = null;

            OrderHeaderDTO orderHeader = await dbContext.OrderHeaders
                .FirstOrDefaultAsync(o => o.OrderHeaderId == orderHeaderId);

            Debug.WriteLine(orderHeaderId);

            if (orderHeader == null)
            {
                shoppingEvent = new ShoppingFailedEvent("order header error");
                return shoppingEvent;
            }
            Debug.WriteLine("OK");

            var orderLines = dbContext.OrderLines
                .Where(ol => ol.OrderHeaderId == orderHeader.OrderHeaderId);

            if (orderLines == null)
            {
                shoppingEvent = new ShoppingFailedEvent("order line error");
                return shoppingEvent;
            }
            Debug.WriteLine("OK");

            var pdfFilePath = $"D:\\Order_{orderHeaderId}.pdf";

            using (var pdfWriter = new PdfWriter(pdfFilePath))
            {
                using (var pdf = new PdfDocument(pdfWriter))
                {
                    var document = new Document(pdf);

                    var title = new Paragraph("Order Details\n\n")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(20)
                        .SetBold();
                    document.Add(title);

                    var orderHeaderSection = new Paragraph()
                        .Add(new Text($"Order ID: {orderHeader.OrderHeaderId}\n\n").SetBold())
                        .Add(new Text($"Client: {orderHeader.FirstName} {orderHeader.LastName}\n\n").SetBold())
                        .Add(new Text($"Address: {orderHeader.Address}\n\n").SetBold())
                        .Add(new Text($"Phone Number: 0{orderHeader.PhoneNumber}\n\n").SetBold())
                        .Add(new Text($"Date: {orderHeader.Date}\n\n\n\n").SetBold());

                    document.Add(orderHeaderSection);

                    document.Add(new Paragraph("Order Lines:\n\n").SetTextAlignment(TextAlignment.RIGHT));
                    foreach (var orderLine in orderLines)
                    {
                        document.Add(new Paragraph($"Product: {orderLine.ProductId}\n\nQuantity: {orderLine.Quantity}\n\nTotal Price: {orderLine.TotalPrice}")
                            .SetTextAlignment(TextAlignment.RIGHT));
                    }

                    document.Close();

                    Console.WriteLine($"PDF generated successfully: {pdfFilePath}");
                    if (File.Exists(pdfFilePath))
                    {
                        Process.Start(new ProcessStartInfo(pdfFilePath) { UseShellExecute = true });
                    }


                }
            }
            shoppingEvent = new ShoppingSucceedEvent();

            return shoppingEvent;
        }
    }
}
