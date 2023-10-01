Public Class User

    Private userName As String
    Private password As String
    Private fullName As String
    Private role As String
    Private pending As Boolean
    Private birthDate As Date
    Private age As Integer

    ' username
    Public Sub SetUserName(username As String)
        Me.userName = username
    End Sub

    Public Function GetUserName() As String
        Return Me.userName
    End Function

    ' password
    Public Sub SetPassword(password As String)
        Me.password = password
    End Sub

    Public Function GetPassword() As String
        Return Me.password
    End Function

    ' role
    Public Sub SetRole(role As String)
        Me.role = role
    End Sub

    Public Function GetRole() As String
        Return Me.role
    End Function


    Public Sub SetFullName(fullName As String)
        Me.fullName = fullName
    End Sub

    Public Function GetFullName() As String
        Return Me.fullName
    End Function

    Public Sub SetPending(pending As Boolean)
        Me.pending = pending
    End Sub

    Public Function GetPending() As Boolean
        Return Me.pending
    End Function

    ' age
    Public Sub SetAge(age As Integer)
        Me.age = age
    End Sub

    Public Function GetAge() As Integer
        Return Me.age
    End Function

    ' age
    Public Sub SetBirthDate(birthDate As Date)
        Me.birthDate = birthDate
    End Sub

    Public Function GetBirthDate() As Date
        Return Me.birthDate
    End Function

End Class
