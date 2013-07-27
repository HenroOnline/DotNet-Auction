using Auction.BLL.Repositories;
using Auction.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Auction.BLL
{
		public class FileHelper
		{
				public static int SaveFileAttachment(int fileAttachmentId, HttpPostedFile httpPostedFile)
				{
						return SaveFileAttachment(fileAttachmentId, httpPostedFile.InputStream, Path.GetFileName(httpPostedFile.FileName), httpPostedFile.ContentType);
				}

				public static int SaveFileAttachment(int fileAttachmentId, Stream stream, string name, string contentType)
				{
						int result = 0;

						try
						{
								var dataRootFolder = Config.DataRootFolder;

								if (!string.IsNullOrEmpty(dataRootFolder))
								{
										var fileAttachmentRootFolder = System.IO.Path.Combine(dataRootFolder, "FileAttachment");
										if (!System.IO.Directory.Exists(fileAttachmentRootFolder))
										{
												System.IO.Directory.CreateDirectory(fileAttachmentRootFolder);
										}

										string extension = Path.GetExtension(name).Replace(".", string.Empty);

										var ent = FileAttachmentRepository.Instance.Select(fileAttachmentId) ?? FileAttachmentRepository.Instance.NewEntity();

										ent.Name = name;
										ent.SizeInBytes = (Int32)stream.Length;
										ent.ContentType = contentType;
										ent.Extension = extension;

										FileAttachmentRepository.Instance.Save(ent);

										FileStream fs = null;
										try
										{
												var storedFileName = System.IO.Path.Combine(fileAttachmentRootFolder, String.Format("{0}_{1}", ent.Id, ent.Name));

												byte[] data = new byte[ent.SizeInBytes];
												stream.Read(data, 0, ent.SizeInBytes);

												fs = new FileStream(storedFileName, FileMode.Create);
												fs.Write(data, 0, data.Length);
												fs.Close();

												result = ent.Id;
										}
										catch (Exception)
										{
												result = 0;

												if (fs != null)
												{
														fs.Close();
												}
										}
								}
						}
						catch (Exception) { }

						return result;
				}

				public static byte[] SelectDataFromFileAttachment(int fileAttachmentId)
				{
							return SelectDataFromFileAttachment(fileAttachmentId, Size.Empty);
				}

				public static byte[] SelectDataFromFileAttachment(int fileAttachmentId, Size size)
				{
						byte[] result = null;

						var dataRootFolder = Config.DataRootFolder;

						if (string.IsNullOrEmpty(dataRootFolder))
						{
								return result;
						}

						var fileAttachmentRootFolder = System.IO.Path.Combine(dataRootFolder, "FileAttachment");
						if (!System.IO.Directory.Exists(fileAttachmentRootFolder))
						{
								return result;
						}

						var fileAttachment = FileAttachmentRepository.Instance.Select(fileAttachmentId);

						if (fileAttachment == null)
						{
								return result;
						}

						var fileAttachmentFolder = System.IO.Path.Combine(fileAttachmentRootFolder, fileAttachment.Id.ToString());
						if (!System.IO.Directory.Exists(fileAttachmentFolder))
						{
								return result;
						}

						return ReadFromDisk(fileAttachment.Name, fileAttachment.Extension, fileAttachmentFolder, size);
				}

				public static byte[] ReadFromDisk(string fileName, string fileExtension, string fileAttachmentFolder, Size size)
				{
						var originalFileName = System.IO.Path.Combine(fileAttachmentFolder, string.Format("{0}.{1}", fileName, fileExtension));
						var fileNameToRetrieve = originalFileName;

						if (size != Size.Empty)
						{
								fileNameToRetrieve = System.IO.Path.Combine(fileAttachmentFolder, string.Format("{0}_{1}_{2}.{3}", fileName, size.Height, size.Width, fileExtension));
						}

						byte[] result = null;

						if (size != Size.Empty && !System.IO.File.Exists(fileNameToRetrieve))
						{
								var resizedImage = ResizeImage(Image.FromFile(originalFileName), size);

								if (resizedImage == null)
								{
										return result;
								}

								resizedImage.Save(fileNameToRetrieve);
						}

						using (var fileStream = new FileStream(fileNameToRetrieve, FileMode.Open, FileAccess.Read))
						{
								Int32 length = (Int32)fileStream.Length;
								result = new Byte[length];
								Int32 count;
								Int32 sum = 0;

								while ((count = fileStream.Read(result, sum, length - sum)) > 0)
								{
										sum += count;
								}
						}

						return result;
				}

				public static Image ResizeImage(Image image, Size size, bool preserveAspectRatio = true)
				{
						try
						{
								int originalWidth = image.Width;
								int originalHeight = image.Height;

								if (originalWidth < size.Width)
								{
										size.Width = originalWidth;
								}

								if (originalHeight < size.Height)
								{
										size.Height = originalHeight;
								}

								int newWidth;
								int newHeight;
								if (preserveAspectRatio)
								{										
										float percentWidth = (float)size.Width / (float)originalWidth;
										float percentHeight = (float)size.Height / (float)originalHeight;
										float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
										newWidth = (int)(originalWidth * percent);
										newHeight = (int)(originalHeight * percent);
								}
								else
								{
										newWidth = size.Width;
										newHeight = size.Height;
								}
								Image newImage = new Bitmap(newWidth, newHeight);
								using (Graphics graphicsHandle = Graphics.FromImage(newImage))
								{
										graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
										graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
								}
								return newImage;
						}
						catch (Exception)
						{
								return null;
						}
				}
		}
}
