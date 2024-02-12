using DinkToPdf;
using DinkToPdf.Contracts;

namespace CertificationGenerator.Services
{
    public class PdfGenerator
    {
        private readonly IConverter _converter;

        public PdfGenerator(IConverter converter)
        {
            this._converter = converter;
        }

        public byte[] GeneratorPdf(string htmlContent)
        {
            // Add CSS styles for the certificate
            string certificateStyles = @"
                <style>
                    .certificate {
                        border: 20px solid #0C5280;
                        padding: 25px;
                        height: 600px;
                        position: relative;
                    }
                    .certificate:after {
                        content: '';
                        top: 0px;
                        left: 0px;
                        bottom: 0px;
                        right: 0px;
                        position: absolute;
                        background-image: url(https://image.ibb.co/ckrVv7/water_mark_logo.png);
                        background-size: 100%;
                        z-index: -1;
                    }
                    .certificate-header > .logo {
                        width: 80px;
                        height: 80px;
                    }
                    .certificate-title {
                        text-align: center;
                    }
                    .certificate-body {
                        text-align: center;
                    }
                    h1 {
                        font-weight: 400;
                        font-size: 48px;
                        color: #0C5280;
                    }
                    .student-name {
                        font-size: 24px;
                    }
                    .certificate-content {
                        margin: 0 auto;
                        width: 750px;
                    }
                    .about-certificate {
                        width: 380px;
                        margin: 0 auto;
                    }
                    .topic-description {
                        text-align: center;
                    }
                </style>
            ";

            // Combine certificate styles with HTML content
            string htmlContentWithoutSpaces = htmlContent.Trim();

            string htmlWithStyles = certificateStyles + htmlContentWithoutSpaces;

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Landscape,
                DocumentTitle = "title",
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlWithStyles,
                //Page = _hostingEnvironment.ContentRootPath + "htmlpagenew.html",
                WebSettings = { DefaultEncoding = "utf-8" },
            };

            var document = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            return _converter.Convert(document);
        }
    }
}
