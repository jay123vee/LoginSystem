Imports System.IO
Imports Npgsql

Public Class DatabaseService
    Dim connectionString As String = "Host=localhost;Port=5432;Database=new_user_table;Username=postgres;Password=1234"

    Public Function saveUserToDataBase(user As User) As Boolean
        Dim existingUser = getUserByUserName(user.GetUserName)

        'If existingUser Is Nothing Then
        Dim insertQuery As String = "INSERT INTO user_table (username, password, full_name, role, pending, birth_date, age) VALUES (@username, @password, @full_name, @role, @pending, @birth_date, @age);"

            Using connection As New NpgsqlConnection(connectionString)
                Using cmd As New NpgsqlCommand(insertQuery, connection)
                    ' Set the parameter values
                    cmd.Parameters.AddWithValue("@username", user.GetUserName())
                    cmd.Parameters.AddWithValue("@password", user.GetPassword())
                    cmd.Parameters.AddWithValue("@full_name", user.GetFullName())
                    cmd.Parameters.AddWithValue("@role", user.GetRole())
                    cmd.Parameters.AddWithValue("@pending", user.GetPending())
                    cmd.Parameters.AddWithValue("@birth_date", user.GetBirthDate())
                    cmd.Parameters.AddWithValue("@age", user.GetAge())

                    connection.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            Dim newUser = getUserByUserName(user.GetUserName)

            If newUser Is Nothing Then
                Return False
            Else
                Return True
            End If
        'End If
        Return False

    End Function

    Public Function getUserByUserName(userName As String) As User
        Dim user As New User()

        ' Your PostgreSQL connection string

        ' Construct the SQL query
        Dim selectQuery As String = "SELECT * FROM user_table WHERE username = @username"

        Using connection As New NpgsqlConnection(connectionString)
            connection.Open()

            Using cmd As New NpgsqlCommand(selectQuery, connection)
                cmd.Parameters.AddWithValue("@username", userName)

                Using reader As NpgsqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        ' Populate the User object with retrieved data
                        user.SetUserName(reader.GetString(reader.GetOrdinal("username")))
                        user.SetPassword(reader.GetString(reader.GetOrdinal("password")))
                        user.SetFullName(reader.GetString(reader.GetOrdinal("full_name")))
                        user.SetRole(reader.GetString(reader.GetOrdinal("role")))
                        user.SetPending(reader.GetBoolean(reader.GetOrdinal("pending")))
                        user.SetBirthDate(reader.GetDateTime(reader.GetOrdinal("birth_date")))
                    End If
                End Using
            End Using
        End Using
        Return user
    End Function
End Class
