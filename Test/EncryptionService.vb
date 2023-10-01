Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Class EncryptionService



    Dim keyString As String = "7E 7B 42 2D E2 32 57 02 75 39 46 71 E0 3D EF 49"
    Dim hexString As String = "1E 2B 32 4D 52 62 77 08 75 39 46 71 E0 3D EF 49"




    Public Function GenerateRandomKey() As Byte()
        ' Remove any spaces from the input string
        Dim tempString = keyString.Replace(" ", "")




        ' Create a Byte array to store the converted values
        Dim byteArray As New List(Of Byte)()

        ' Convert each pair of hex characters to a byte and add it to the array
        For i As Integer = 0 To tempString.Length - 1 Step 2
            Dim hexPair As String = tempString.Substring(i, 2)
            Dim byteValue As Byte = Convert.ToByte(hexPair, 16)
            byteArray.Add(byteValue)
        Next

        ' Convert the List(Of Byte) to a Byte array and return it
        Return byteArray.ToArray()
    End Function




    Public Function GenerateRandomIV() As Byte()
        ' Remove any spaces from the input string
        Dim tempString = hexString.Replace(" ", "")




        ' Create a Byte array to store the converted values
        Dim byteArray As New List(Of Byte)()

        ' Convert each pair of hex characters to a byte and add it to the array
        For i As Integer = 0 To tempString.Length - 1 Step 2
            Dim hexPair As String = tempString.Substring(i, 2)
            Dim byteValue As Byte = Convert.ToByte(hexPair, 16)
            byteArray.Add(byteValue)
        Next

        ' Convert the List(Of Byte) to a Byte array and return it
        Return byteArray.ToArray()
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
