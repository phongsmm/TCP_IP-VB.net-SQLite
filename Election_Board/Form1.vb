Imports System.Data.SQLite



Public Class Form1


    Dim dbpath = Application.StartupPath
    Dim dbname = "Data.db"
    Dim constr As String = String.Format("Data Source = {0}", System.IO.Path.Combine(dbpath, dbname))

    Dim username, password, privilege As New List(Of String)

    Dim loginopen As Boolean = False
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Not loginopen Then
            Panel_Login.Show()
            loginopen = True
        Else
            Panel_Login.Hide()
            loginopen = False
        End If


    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        For Each x In username
            If username.Contains(TextBox1.Text) Then
                If TextBox1.Text = x Then
                    If TextBox2.Text = password(username.IndexOf(x)) Then
                        If privilege(username.IndexOf(x)) = "User" Then
                            Dim f2 As New Form2
                            f2.Show()
                        ElseIf privilege(username.IndexOf(x)) = "Admin" Then
                            MessageBox.Show("Welcome Admin")
                        End If

                    Else
                        MessageBox.Show("Username หรือ Password ไม่ถูกต้อง")
                    End If
                End If
            Else
                MessageBox.Show("Username หรือ Password ไม่ถูกต้อง")
                Exit For
            End If

        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim conn As New SQLiteConnection(constr)

        conn.Open()

        Dim sql As String = "select * from Users"
        Dim cmd As New SQLiteCommand(sql, conn)
        Dim reader As SQLiteDataReader = cmd.ExecuteReader

        While reader.Read
            username.Add(reader(1).ToString)
            password.Add(reader(2).ToString)
            privilege.Add(reader(3).ToString)

        End While

        conn.Close()







    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


    End Sub


End Class
