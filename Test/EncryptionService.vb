Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Class EncryptionService

    Public Function GenerateRandomKey() As Byte()
        Dim aesAlg As New AesCryptoServiceProvider()
        aesAlg.GenerateKey()
        Return aesAlg.Key
    End Function

    Public Function GenerateRandomIV() As Byte()
        Dim aesAlg As New AesCryptoServiceProvider()
        aesAlg.GenerateIV()
        Return aesAlg.IV
    End Function

    Public Function EncryptText(inputText As String, key As Byte(), iv As Byte()) As String
        Using aesAlg As New AesCryptoServiceProvider()
            aesAlg.Key = key
            aesAlg.IV = iv
            Dim encryptor As ICryptoTransform = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)
            Using msEncrypt As New MemoryStream()
                Using csEncrypt As New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)
                    Using swEncrypt As New StreamWriter(csEncrypt)
                        swEncrypt.Write(inputText)
                    End Using
                End Using
                Return Convert.ToBase64String(msEncrypt.ToArray())
            End Using
        End Using
    End Function

    Public Function DecryptText(encryptedText As String, key As Byte(), iv As Byte()) As String
        Using aesAlg As New AesCryptoServiceProvider()
            aesAlg.Key = key
            aesAlg.IV = iv
            Dim decryptor As ICryptoTransform = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV)
            Using msDecrypt As New MemoryStream(Convert.FromBase64String(encryptedText))
                Using csDecrypt As New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)
                    Using srDecrypt As New StreamReader(csDecrypt)
                        Return srDecrypt.ReadToEnd()
                    End Using
                End Using
            End Using
        End Using
    End Function

End Class
