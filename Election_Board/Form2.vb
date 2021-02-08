Imports System.Net.Sockets
Imports System.Net
Imports System.Text
Imports System.IO

Public Class Form2


    Dim Client As TcpClient
    Dim RX As StreamReader
    Dim TX As StreamWriter


    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Click

    End Sub

    Public Sub SendStream()
        Try
            Client = New TcpClient("192.168.1.55", 8000)
            If Client.GetStream.CanRead Then
                RX = New StreamReader(Client.GetStream)
                TX = New StreamWriter(Client.GetStream)
                Threading.ThreadPool.QueueUserWorkItem(AddressOf Connected)

            End If
        Catch ex As Exception

        End Try

    End Sub

    Function Connected()
        If RX.BaseStream.CanRead Then
            Try
                While RX.BaseStream.CanRead
                    Dim RawData As String = RX.ReadLine
                    If RawData.ToUpper = "/MSG" Then
                        Threading.ThreadPool.QueueUserWorkItem(AddressOf MSG1, "Connected Successfully")
                    Else
                        MessageBox.Show(RawData, "Sever Say : ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End While
            Catch ex As Exception
                Client.Close()
            End Try
        End If

        Return True

    End Function

    Function MSG1(Data As String)
        Return MessageBox.Show(Data)

    End Function

    Sub SendToServer(Data As String)
        Try
            TX.WriteLine(Data)
            TX.Flush()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SendStream()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim msg = TextBox1.Text & "," & TextBox2.Text & "," & TextBox3.Text
        SendToServer(msg)
    End Sub
End Class