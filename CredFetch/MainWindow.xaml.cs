using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
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

        var buttonAmount = CountButtons();
        if (buttonAmount == 0) return;

        var buttons = CreateButtons(buttonAmount);
        ConfigureWindowPlaceButtons(buttons);
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

    private int CountButtons()
    {
        var keyAmount = _eachKey.Count;
        var valueAmount = _eachValue.Count;

        if (keyAmount < 1 || valueAmount < 1 || keyAmount != valueAmount)
        {
            MessageBox.Show(Constants.ConfigMismatch);
            return 0;
        }

        if (keyAmount <= 15 && valueAmount <= 15) return keyAmount;
        MessageBox.Show(Constants.PasswordLimit);
        return 0;
    }

    private List<Button> CreateButtons(int elements)
    {
        var buttonList = new List<Button>();

        for (var i = 0; i < elements; i++)
        {
            var button = new Button
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 35,
                Width = 150,
                FontSize = 14,
                IsEnabled = true,
                Content = _eachKey[i],
                Name = $"{_eachKey[i]}{i}"
            };
            button.Click += BtnClicked;
            buttonList.Add(button);
        }

        return buttonList;
    }

    private void ConfigureWindowPlaceButtons(List<Button> buttons)
    {
        var topMargin = 15;
        foreach (var btn in buttons)
        {
            btn.Margin = new Thickness(0, topMargin, 0, 15);
            WindowMain.GridLayout.Children.Add(btn);
            topMargin += 40;
        }

        KeyBox.Password = "";
        KeyBox.Visibility = Visibility.Hidden;

        IvBox.Password = "";
        IvBox.Visibility = Visibility.Hidden;

        ButtonOk.IsEnabled = false;
        ButtonOk.Visibility = Visibility.Hidden;

        WindowMain.SizeToContent = SizeToContent.Height;
        WindowMain.Width = 210;
        WindowMain.ResizeMode = ResizeMode.CanMinimize;
    }

    private void BtnClicked(object sender, RoutedEventArgs e)
    {
        var name = Convert.ToString(e.Source.GetType().GetProperty("Name")?.GetValue(e.Source, null));

        if (name == null)
        {
            MessageBox.Show(Constants.ConfigMismatch);
            return;
        }

        var number = Regex.Match(name, @"\d+").Value;
        var num = int.Parse(number);
        Clipboard.SetText(_eachValue[num]);
    }
}