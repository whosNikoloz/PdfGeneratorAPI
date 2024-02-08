using CertificationGenerator.Services;
using Microsoft.AspNetCore.Mvc;
using CertificationGenerator.Models;

namespace CertificationGenerator.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PDFController : Controller
    {

        private readonly PdfGenerator _pdfGenerator;
        public PDFController(PdfGenerator pdfGenerator)
        {
            this._pdfGenerator = pdfGenerator;
        }


        [HttpPost("Generatepdf")]
        public IActionResult GeneratePDf(requestModel req)
        {

            string HtmlContent = "<html><body><h1>Hello Nika!</h1>" +
                "<img src ='https://firebasestorage.googleapis.com/v0/b/eduspace-a81b5.appspot.com/o/EduSpaceLogo.png?alt=media&token=7b7dc8a5-05d8-4348-9b4c-c19913949c67'" +
                "alt='Example Image'></body></html>";

            byte[] pdfBytes = _pdfGenerator.GeneratorPdf(HtmlContent);

            return File(pdfBytes, "application/pdf", "generated.pdf");

        }
    }
}
