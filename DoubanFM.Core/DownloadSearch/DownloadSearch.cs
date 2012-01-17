﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoubanFM.Core
{
	/// <summary>
	/// 下载搜索
	/// </summary>
	public static class DownloadSearch
	{
		/// <summary>
		/// 设置
		/// </summary>
		public static Settings Settings { get; internal set; }
		/// <summary>
		/// 搜索
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="artist">艺术家</param>
		/// <param name="album">专辑</param>
		public static void Search(string title, string artist, string album)
		{
			if (title == null) title = string.Empty;
			if (artist == null) artist = string.Empty;
			if (album == null) album = string.Empty;
			string keyword = GetKeyword(title, artist, album);
			if (Settings.DownloadSite.HasFlag(DownloadSite.GoogleMusic))
			{
				GoogleMusicSearch(keyword);
			}
			if (Settings.DownloadSite.HasFlag(DownloadSite.BaiduTing))
			{
				BaiduTingSearch(keyword);
			}
		}
		/// <summary>
		/// 搜索谷歌音乐
		/// </summary>
		/// <param name="keyword">关键词</param>
		private static void GoogleMusicSearch(string keyword)
		{
			Parameters parameters = new Parameters();
			parameters.Add("q", keyword);
			string url = ConnectionBase.ConstructUrlWithParameters("http://www.google.cn/music/search", parameters);
			UrlHelper.OpenLink(url);
		}

		/// <summary>
		/// 搜索百度听
		/// </summary>
		/// <param name="keyword">关键词</param>
		private static void BaiduTingSearch(string keyword)
		{
			Parameters parameters = new Parameters();
			parameters.Add("key", keyword);
			string url = ConnectionBase.ConstructUrlWithParameters("http://ting.baidu.com/search", parameters);
			UrlHelper.OpenLink(url);
		}

		/// <summary>
		/// 获取用于搜索的关键词
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="artist">艺术家</param>
		/// <param name="album">专辑</param>
		/// <returns></returns>
		private static string GetKeyword(string title, string artist, string album)
		{
			if (album.EndsWith("..."))
			{
				album = album.Substring(0, album.Length - 3);
			}
			if (Settings.TrimBrackets)
			{
				title = TrimBrackets(title);
				artist = TrimBrackets(artist);
				album = TrimBrackets(album);
			}
			if (Settings.SearchAlbum)
			{
				return string.Format("{0} {1} {2}", title, artist, album);
			}
			else
			{
				return string.Format("{0} {1}", title, artist);
			}
		}

		private static readonly List<char> brackets = new List<char> { '(', '（', '[', '【'};
			
		/// <summary>
		/// 剔除括号内的内容
		/// </summary>
		/// <param name="someString">任意字符串</param>
		/// <returns></returns>
		private static string TrimBrackets(string someString)
		{
			int index;

			foreach (var bracket in brackets)
			{
				index = someString.IndexOf(bracket);
				if (index != -1)
				{
					someString = someString.Substring(0, index);
				}
			}

			someString = someString.Trim();
			
			return someString;
		}
	}
}
