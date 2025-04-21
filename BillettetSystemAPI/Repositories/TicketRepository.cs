using ModelBilletterSystem;
using BillettetSystemAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using ModelLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Image;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace BillettetSystemAPI.Repositories
{


    public class TicketRepository : ITicket
    {
        private readonly ApplicationDbContext _context;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<List<buyTicketResponse>> BuyTicket(int eventId, int TicketIntotal)
        {
            var amountOfSoldTicketsForEvent = await _context.Tickets
              .CountAsync(e => e.EventId == eventId);

            var maximumTicketAmountForEvent = (await _context.Events.SingleAsync(e => e.Id_event == eventId)).Ticket_Amount;


            if (amountOfSoldTicketsForEvent == null)
                throw new Exception("Event not found.");

            if ((amountOfSoldTicketsForEvent + TicketIntotal) >= maximumTicketAmountForEvent)
                throw new Exception("All tickets for this event have been sold out.");

            var tickets = new List<buyTicketResponse>();
            for (int i = 0; i < TicketIntotal; i++)
            {
                var ticket = new Ticket
                {
                    Id_Ticket = Guid.NewGuid(),
                    is_used = false,
                    EventId = eventId
                };
                _context.Tickets.Add(ticket);
                tickets.Add(new buyTicketResponse()
                {
                    Id_Ticket = ticket.Id_Ticket,
                    QrCodeBase64 = GenerateQrCode(ticket.Id_Ticket.ToString())
                });
            }

            await _context.SaveChangesAsync();

            return tickets;
        }

        private string GenerateQrCode(string content)
        {
            using var generator = new QRCodeGenerator();
            using var data = generator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new BitmapByteQRCode(data);
            byte[] qrCodeImage = qrCode.GetGraphic(20);
            return Convert.ToBase64String(qrCodeImage);
        }




        public Task<List<Ticket>> GetAllTickets()
        {
            throw new NotImplementedException();
        }

        public Task<Ticket> GetTicketById(Guid Id_ticket)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveTicket(Guid Id_ticket)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> MarkAsUsed(Guid id_ticket)
        {
            try
            {
                var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id_Ticket == id_ticket);
                if (ticket == null)
                    return new NotFoundResult();

                if (ticket.is_used)
                    return new BadRequestResult();

                ticket.is_used = true;
                _context.Tickets.Update(ticket);

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
              

            }
        }

        public async Task<IActionResult> TicketPDF(Guid id_ticket)
        {
            var ticket = await _context.Tickets.FindAsync(id_ticket);
            if (ticket == null)
                return new NotFoundResult();

            var qrBase64 = GenerateQrCode(id_ticket.ToString());
            byte[] qrImageBytes = Convert.FromBase64String(qrBase64);

            using (var ms = new MemoryStream())
            {
                var pdf = new PdfDocument(new PdfWriter(ms));
                var doc = new Document(pdf);

                doc.Add(new Paragraph("🎟 Event Ticket"));
                doc.Add(new Paragraph($"Ticket ID: {ticket.Id_Ticket}"));
                doc.Add(new Paragraph($"Event ID: {ticket.EventId}"));
                doc.Add(new Paragraph($"Issued On: {DateTime.Now}"));

               

                var qrImage = new Image(ImageDataFactory.Create(qrImageBytes));
                qrImage.ScaleToFit(200, 200);
                doc.Add(qrImage);

                doc.Close();
                var pdfBytes = ms.ToArray();
                return new FileContentResult(pdfBytes, "application/pdf")
                {
                    FileDownloadName = "Ticket.pdf"
                };
            }
        }


        private IActionResult NotFoundResult()
        {
            throw new NotImplementedException();
        }

        private IActionResult BadRequest(string v)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTicket(Guid id_Ticket, bool is_used, int eventId)
        {
            throw new NotImplementedException();
        }

        Task<Ticket> ITicket.UpdateTicket(Guid Id_ticket, bool is_used, int eventId)
        {
            throw new NotImplementedException();
        }
    }
}

    