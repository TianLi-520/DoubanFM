﻿/*
 * Author : K.F.Storm
 * Email : yk000123 at sina.com
 * Website : http://www.kfstorm.com
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DoubanFM.Core;
using System.Windows.Media.Animation;

namespace DoubanFM.NotifyIcon
{
	/// <summary>
	/// 托盘控制面板
	/// </summary>
	public partial class PopupControlPanel : UserControl
	{
		public PopupControlPanel()
		{
			InitializeComponent();

			_player = FindResource("Player") as Player;
		}

		/// <summary>
		/// 平滑地显示封面
		/// </summary>
		public void ShowCoverSmooth()
		{
			((Storyboard)FindResource("ShowCoverSmooth")).Begin();
		}
		/// <summary>
		/// 隐藏封面
		/// </summary>
		public void HideCover()
		{
			((Storyboard)FindResource("HideCover")).Begin();
		}

		private Player _player;

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			(App.Current.MainWindow as DoubanFMWindow).Next();
		}

		private void ButtonNever_Click(object sender, RoutedEventArgs e)
		{
			(App.Current.MainWindow as DoubanFMWindow).Never();
		}

		private void ShareButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			// 在此处添加事件处理程序实现。
			if (_player.CurrentSong != null)
				new Share(_player, (Share.Sites)((FrameworkElement)e.Source).Tag).Go();
		}

		private void CheckBoxShowLyrics_Checked(object sender, System.Windows.RoutedEventArgs e)
		{
			// 在此处添加事件处理程序实现。
			if (_player != null) (App.Current.MainWindow as DoubanFMWindow).ShowLyrics();
		}

		private void CheckBoxShowLyrics_Unchecked(object sender, RoutedEventArgs e)
		{
			if (_player != null) (App.Current.MainWindow as DoubanFMWindow).HideLyrics();
		}

		private void BtnCopyUrl_Click(object sender, RoutedEventArgs e)
		{
			if (_player.CurrentSong != null)
			{
				new Share(_player).Go();
                MessageBox.Show(App.Current.MainWindow, DoubanFM.Resources.Resources.UrlCopyedToClipboard, DoubanFM.Resources.Resources.SuccessfullyCopied, MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private void BtnOneKeyShare_Click(object sender, RoutedEventArgs e)
		{
			(App.Current.MainWindow as DoubanFMWindow).OneKeyShare();
		}
		
		private void ButtonExit_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			App.Current.MainWindow.Close();
		}

		private void BtnDownloadSearch_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			(App.Current.MainWindow as DoubanFMWindow).SearchDownload();
		}
	}
}
