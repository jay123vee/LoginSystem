Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel

Public Class DatabaseService
    Dim filePath As String = Path.Combine(Application.StartupPath, "Users.txt")

    Public Function saveUserToDataBase(user As User) As Boolean
        'save user to database
        ' Append the registration details to a text file (you can change the file path)
        Try
            Using writer As New System.IO.StreamWriter(filePath, True)
                writer.WriteLine($"{user.GetUserName},{user.GetPassword},{user.GetFullName},{user.GetBirthDate},{user.GetAge},{user.GetRole},{user.GetPending}")
            End Using

            Return True ' Registration successful
        Catch ex As Exception
            Return False ' Registration failed
        End Try
    End Function

    Public Function getUserByUserName(userName As String) As User
        'get user from datbase by searching database

        Return New User()
    End Function
End Class
