Imports System
Imports System.IO
Imports System.Globalization
Imports Silanis.ESL.SDK
Imports Silanis.ESL.SDK.Builder

Namespace SDK.VBExamples
    Public Module PackageLanguageExample
        Dim apiToken = "YOUR TOKEN HERE"
        Dim baseUrl = "ENVIRONMENT URL HERE"

        Sub Main()
            ' Create new esl client with api token and base url
            Dim client = New EslClient(apiToken, baseUrl)
            Dim file = New FileInfo(Directory.GetCurrentDirectory() + My.Resources.document)

            Dim package = PackageBuilder.NewPackageNamed("VB Package " + DateTime.Now) _
                .DescribedAs("This is a new package") _
                .WithLanguage(New CultureInfo("fr")) _
                .WithSigner(SignerBuilder.NewSignerWithEmail("signer@email.com") _
                    .WithFirstName("John") _
                    .WithLastName("Smith")) _
                .WithDocument(DocumentBuilder.NewDocumentNamed("My Document") _
                    .FromFile(file.FullName) _
                    .WithSignature(SignatureBuilder.SignatureFor("signer@email.com") _
                        .OnPage(0) _
                        .AtPosition(500, 100))) _
                .Build()

            Dim id = client.CreatePackage(package)

            client.SendPackage(id)

            Console.WriteLine("Package {0} was sent", id.Id)
        End Sub
    End Module
End Namespace