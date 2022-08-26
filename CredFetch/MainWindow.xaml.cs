using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using CredFetch.Models;

namespace CredFetch;

public partial class MainWindow
{
    private readonly List<string> _eachKey = new();
    private readonly List<string> _eachValue = new();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void DecryptConfigFileBtn(object sender, RoutedEventArgs e)
    {
        CheckFileExists();
        var fileContent = ReadFile();
        var decryptedFile = DecryptFile(fileContent);
        GetAllKeysAndValues(decryptedFile);
        EnableAndSetButtons();
    }

    private static void CheckFileExists()
    {
        if (File.Exists(Constants.ConfigFileName)) return;
        MessageBox.Show(Constants.NoConfigFile);
        throw new Exception(Constants.NoConfigFile);
    }

    private static string ReadFile()
    {
        return File.ReadAllText(Constants.ConfigFileName);
    }

    private string DecryptFile(string fileContent)
    {
        return DecryptService.DecryptStringFromBytes(Convert.FromBase64String(fileContent),
            Encoding.ASCII.GetBytes(KeyBox.Password), Encoding.ASCII.GetBytes(IvBox.Password));
    }

    private void GetAllKeysAndValues(string decryptedFile)
    {
        var finalConfig = ConfigProperties.FromJson(decryptedFile);

        foreach (var item in finalConfig!)
        {
            _eachKey.Add(item.Key);
            _eachValue.Add(item.Value);
        }
    }

    private void EnableAndSetButtons()
    {
        Button0.IsEnabled = true;
        Button0.Content = _eachKey[0];
        Button1.IsEnabled = true;
        Button1.Content = _eachKey[1];
        Button2.IsEnabled = true;
        Button2.Content = _eachKey[2];
        Button3.IsEnabled = true;
        Button3.Content = _eachKey[3];
        Button4.IsEnabled = true;
        Button4.Content = _eachKey[4];
        Button5.IsEnabled = true;
        Button5.Content = _eachKey[5];
        Button6.IsEnabled = true;
        Button6.Content = _eachKey[6];
        Button7.IsEnabled = true;
        Button7.Content = _eachKey[7];
        Button8.IsEnabled = true;
        Button8.Content = _eachKey[8];
        Button9.IsEnabled = true;
        Button9.Content = _eachKey[9];
        Button10.IsEnabled = true;
        Button10.Content = _eachKey[10];
        Button11.IsEnabled = true;
        Button11.Content = _eachKey[11];
        Button12.IsEnabled = true;
        Button12.Content = _eachKey[12];

        KeyBox.IsEnabled = false;
        KeyBox.Password = "";

        IvBox.IsEnabled = false;
        IvBox.Password = "";

        ButtonOk.IsEnabled = false;
    }

    private void BtnOne(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(_eachValue[0]);
    }

    private void BtnTwo(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(_eachValue[1]);
    }

    private void BtnThree(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(_eachValue[2]);
    }

    private void BtnFour(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(_eachValue[3]);
    }

    private void BtnFive(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(_eachValue[4]);
    }

    private void BtnSix(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(_eachValue[5]);
    }

    private void BtnSeven(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(_eachValue[6]);
    }

    private void BtnEight(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(_eachValue[7]);
    }

    private void BtnNine(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(_eachValue[8]);
    }

    private void BtnTen(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(_eachValue[9]);
    }

    private void BtnEleven(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(_eachValue[10]);
    }

    private void BtnTwelve(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(_eachValue[11]);
    }

    private void BtnThirteen(object sender, RoutedEventArgs e)
    {
        Clipboard.SetText(_eachValue[12]);
    }
}