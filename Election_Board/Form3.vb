Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Data.SQLite

Public Class Form3

    Dim ServerStatus = False
    Dim ServerTry = False
    Dim Server As TcpListener
    Dim Clients As New List(Of TcpClient)

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        StartServer()
    End Sub

    Public Sub StartServer()
        If ServerStatus = False Then

            Try
                Server = New TcpListener(IPAddress.Any, 8000)
                Server.Start()
                ServerStatus = True
                Threading.ThreadPool.QueueUserWorkItem(AddressOf Handler_Client)
            Catch ex As Exception
                ServerStatus = False

            End Try
            ServerStatus = False


        End If

    End Sub

    Public Sub StopServer()
        If ServerStatus = True Then

            Try
                For Each Client As TcpClient In Clients
                    Client.Close()

                Next
                Server.Stop()
                ServerStatus = False


            Catch ex As Exception
                StopServer()

            End Try
            ServerStatus = False


        End If

    End Sub

    Function Handler_Client(ByVal state As Object)
        Dim TempClient As TcpClient

        Try
            Using Client As TcpClient = Server.AcceptTcpClient
                If ServerStatus = False Then
                    Threading.ThreadPool.QueueUserWorkItem(AddressOf Handler_Client)
                End If
                Clients.Add(Client)
                TempClient = Client
                Dim TX As New StreamWriter(Client.GetStream)
                Dim RX As New StreamReader(Client.GetStream)
                If RX.BaseStream.CanRead = True Then
                    While RX.BaseStream.CanRead = True
                        Dim RawData As String = RX.ReadLine
                        Label1.Text = RawData


                    End While
                End If

                If RX.BaseStream.CanRead = False Then
                    Client.Close()
                    Clients.Remove(Client)
                End If

                If Not TempClient.GetStream.CanRead Then
                    TempClient.Close()
                    Clients.Remove(TempClient)
                End If

            End Using



        Catch ex As Exception
            TempClient.Close()
            Clients.Remove(TempClient)


        End Try
        Return True
    End Function


End Class