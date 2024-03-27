using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class UploadService : IUploadService
	{
		public string SaveImageFromBase64(string base64)
		{
			var bmp = new Bitmap(ConvertBase64ToImage(base64));
			return Save(bmp);
		}

		public async Task<string> SaveImageFromURL(string url)
		{
			var bmp = await URLToBitmap(url);
			return Save(bmp);
		}

		public async Task<string> SaveFileFromIFormFile(IFormFile formFile)
		{
			var postedFileExtension = Path.GetExtension(formFile.FileName);
			if (!string.Equals(postedFileExtension, ".jpg", StringComparison.OrdinalIgnoreCase)
				&& !string.Equals(postedFileExtension, ".png", StringComparison.OrdinalIgnoreCase)
				&& !string.Equals(postedFileExtension, ".gif", StringComparison.OrdinalIgnoreCase)
				&& !string.Equals(postedFileExtension, ".jpeg", StringComparison.OrdinalIgnoreCase))
			{
				return await Save(formFile);
			}
			
			var bmp = new Bitmap(await IFormFileToBitmap(formFile));

			return Save(bmp);
		}

		private static async Task<Bitmap> IFormFileToBitmap(IFormFile file)
		{
			using (MemoryStream ms = new())
			{
				await file.CopyToAsync(ms);
				ms.Position = 0;
				Image img = Image.FromStream(ms);
				ms.Close();
				return new Bitmap(img);
			}
		}

		public void RemoveImage(string filename)
		{
			string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", filename);
			if (File.Exists(fullPath))
			{
				File.Delete(fullPath);
			}
		}

		private async Task<string> Save(IFormFile file)
		{
			var fileExtension = Path.GetExtension(file.FileName);
			var fileName = Path.GetRandomFileName() + fileExtension;
			string dirSaveImage = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);

			File.Create(dirSaveImage).Close();
			using FileStream fs = File.OpenWrite(dirSaveImage);
			await file.CopyToAsync(fs);

			return fileName;
		}

		private string Save(Bitmap bmp)
		{
			var fileName = Path.GetRandomFileName() + ".jpg";
			string dirSaveImage = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);
			var saveImage = CompressImage(bmp, bmp.Width, bmp.Height);
			saveImage.Save(dirSaveImage, ImageFormat.Jpeg);
			return fileName;
		}
		private async Task<string> SaveDocument(IFormFile file)
		{
			var fileExtension = Path.GetExtension(file.FileName);
			var fileName = Path.GetRandomFileName() + fileExtension;
			string dirSaveDocument = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);

			using FileStream fs = File.Create(dirSaveDocument);
			await file.CopyToAsync(fs);

			return fileName;
		}
		private Bitmap CompressImage(Bitmap originalPic, int maxWidth, int maxHeight, bool transperent = false)
		{
			try
			{
				int width = originalPic.Width;
				int height = originalPic.Height;
				int widthDiff = width - maxWidth;
				int heightDiff = height - maxHeight;
				bool doWidthResize = (maxWidth > 0 && width > maxWidth && widthDiff > heightDiff);
				bool doHeightResize = (maxHeight > 0 && height > maxHeight && heightDiff > widthDiff);

				if (doWidthResize || doHeightResize || (width.Equals(height) && widthDiff.Equals(heightDiff)))
				{
					int iStart;
					Decimal divider;
					if (doWidthResize)
					{
						iStart = width;
						divider = Math.Abs((Decimal)iStart / maxWidth);
						width = maxWidth;
						height = (int)Math.Round((height / divider));
					}
					else
					{
						iStart = height;
						divider = Math.Abs((Decimal)iStart / maxHeight);
						height = maxHeight;
						width = (int)Math.Round(width / divider);
					}
				}
				using (Bitmap outBmp = new Bitmap(width, height, PixelFormat.Format24bppRgb))
				{
					using (Graphics oGraphics = Graphics.FromImage(outBmp))
					{
						oGraphics.Clear(Color.White);
						oGraphics.DrawImage(originalPic, 0, 0, width, height);

						if (transperent)
						{
							outBmp.MakeTransparent();
						}

						return new Bitmap(outBmp);
					}
				}
			}
			catch
			{
				return null;
			}
		}

		private Image ConvertBase64ToImage(string base64String)
		{
			try
			{
				var parts = base64String.Split(',');

				if (parts.Length == 2)
				{
					var dataPart = parts[1];

					dataPart = dataPart.Trim();
					byte[] imageBytes = Convert.FromBase64String(dataPart);

					using MemoryStream ms = new(imageBytes);

					return Image.FromStream(ms);
				}

				throw new Exception("Current format is not supported.");
			}
			catch (Exception ex)
			{
				throw new Exception("Error during converting to from base64: " + ex.Message);
			}
		}

		private async Task<Bitmap> URLToBitmap(string url)
		{
			using (var client = new HttpClient())
			{
				using (var stream = await client.GetStreamAsync(url))
				{
					return new Bitmap(stream);
				}
			}
		}
	}
}
