Imports System.Text
Imports System.Net
Imports System.IO
Imports System.Threading

Public Class Form1
    Private trd As Thread
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Sub lol()
        Do While True
            Dim riughe As String = "Upc2a6"
            riughe += genstring()
            If Not litecoin("", "{""method"":""walletpassphrase"",""params"":[""" & riughe & """,300],""id"":1}").Contains("Error: The wallet passphrase entered was incorrect.") Then
                MsgBox(riughe)
            End If
        Loop

    End Sub




    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        trd = New Thread(AddressOf lol)
        trd.Start()
    End Sub

    Public Function litecoin(ByVal URL As String, ByVal contnt As String) As String
        Dim gdf As Byte() = Encoding.UTF8.GetBytes(contnt)
        Dim Req As HttpWebRequest
        Dim SourceStream As System.IO.Stream
        Dim Response As HttpWebResponse

        Try

            'create a web request to the URL  
            Req = HttpWebRequest.Create("http://127.0.0.1:1995")
            Req.Credentials = New NetworkCredential("litecoin1337", "bitcoincansucksometimes1")

            Req.ContentType = "application/json-rpc"
            Req.Method = "POST"
            Dim dataStream As Stream = Req.GetRequestStream()
            dataStream.Write(gdf, 0, gdf.Length)
            dataStream.Close()
            Dim webResponlse As WebResponse = Req.GetResponse()
            Dim loResponseStream As StreamReader = New StreamReader(webResponlse.GetResponseStream())
            Return loResponseStream.ReadToEnd()
        Catch ex As WebException
            Try
                Dim errResp As WebResponse = DirectCast(ex, WebException).Response
                Dim loResponseStream As StreamReader = New StreamReader(errResp.GetResponseStream())
                Return loResponseStream.ReadToEnd()

            Catch sdfdg As Exception
                Return "{""result"":null,""error"":{""code"":1337,""message"":""general error""},""id"":null}"
            End Try



        End Try
    End Function
    Public Function genstring(Optional leng As Integer = 10)
        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        Dim r As New Random
        Dim sb As New StringBuilder
        For i As Integer = 1 To leng
            Dim idx As Integer = r.Next(0, (s.Length - 1))
            sb.Append(s.Substring(idx, 1))
        Next
        Return (sb.ToString())
    End Function
End Class
