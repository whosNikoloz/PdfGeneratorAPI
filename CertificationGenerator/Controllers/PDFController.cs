using CertificationGenerator.Services;
using Microsoft.AspNetCore.Mvc;
using CertificationGenerator.Models;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;

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


            var Link = "https://edu-space.vercel.app";


            var qrCodeImg = "http://api.qrserver.com/v1/create-qr-code/?data=" + Link + "&size=100x100";

            string HtmlContent = $@"
            <div class='certificate-container'>
                <div class='certificate'>
                    <div class='water-mark-overlay'></div>
                    <div class='certificate-header'>
                        <img src='https://edu-space.vercel.app/EduSpaceLogo.png' class='logo' alt=''>
                    </div>
                    <div class='certificate-body'>
                        <p class='certificate-title'><strong>RENR NCLEX AND CONTINUING EDUCATION (CME) Review Masters</strong></p>
                        <h1>Certificate of Completion</h1>
                        <p class='student-name'>{req.FirstName} {req.LastName}</p>
                        <div class='certificate-content'>
                            <div class='about-certificate'>
                                <p>
                                    დაამტავრეე :  {req.Subject}
                                </p>
                            </div>
                            <p class='topic-title'>
                                The Topic consists of [hours] Continuity hours and includes the following:
                            </p>
                            <div class='text-center'>
                                <p class='topic-description text-muted'>Contract adminitrator - Types of claim - Claim Strategy - Delay analysis - Thepreliminaries to a claim - The essential elements to a successful claim - Responses - Claim preparation and presentation </p>
                            </div>
                        </div>
                        <img src={qrCodeImg} class='logo' alt=''>
                        <div class='certificate-footer text-muted'>
                            <div class='row'>
                                <div class='col-md-6'>
                                    <p>Principal: ______________________</p>
                                </div>
                                <div class='col-md-6'>
                                    <div class='row'>
                                        <div class='col-md-6'>
                                            <p>
                                                Accredited by
                                            </p>
                                        </div>
                                        <div class='col-md-6'>
                                            <p>
                                                Endorsed by
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    ";

            byte[] pdfBytes = _pdfGenerator.GeneratorPdf(HtmlContent);

            return File(pdfBytes, "application/pdf", "generated.pdf");

        }
    }
}
