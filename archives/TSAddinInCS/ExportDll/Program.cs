using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using ExportDll.Properties;
using ExportDllAttribute;

namespace ExportDll
{
	// Token: 0x02000003 RID: 3
	internal class Program
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static bool FindWithAttribute(MemberInfo mi, object obj)
		{
			return mi.GetCustomAttributes(typeof(ExportDllAttribute.ExportDllAttribute), false).Length != 0;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020B8 File Offset: 0x000002B8
		private static int Main(string[] args)
		{
			try
			{
				if (args.Length < 1)
				{
					Console.WriteLine("Parameter error!");
					return 1;
				}
				bool flag = false;
				if (args.Length > 1 && args[1] == "/Debug")
				{
					flag = true;
				}
				Console.WriteLine("Debug: {0}", flag);
				string text = args[0];
				string directoryName = Path.GetDirectoryName(text);
				if (directoryName == string.Empty)
				{
					Console.WriteLine("Full path needed!");
					return 1;
				}
				if (Path.GetExtension(text) != ".dll")
				{
					Console.WriteLine("Target should be dll!");
					return 1;
				}
				int num = 0;
				Dictionary<string, Dictionary<string, KeyValuePair<string, string>>> dictionary = new Dictionary<string, Dictionary<string, KeyValuePair<string, string>>>();
				Assembly assembly = null;
                using (FileStream fileStream = File.OpenRead(text))
				{
					BinaryReader binaryReader = new BinaryReader(fileStream);
					var bytes = binaryReader.ReadBytes((int)fileStream.Length);
                    assembly = Assembly.Load(bytes);
                    binaryReader.Close();
				}
				foreach (Type type in assembly.GetTypes())
				{
					foreach (MemberInfo memberInfo in type.FindMembers(MemberTypes.All, BindingFlags.Static | BindingFlags.Public, new MemberFilter(Program.FindWithAttribute), null))
					{
						object[] customAttributes = memberInfo.GetCustomAttributes(typeof(ExportDllAttribute.ExportDllAttribute), false);
						if (customAttributes.Length != 0)
						{
                            ExportDllAttribute.ExportDllAttribute exportDllAttribute = customAttributes[0] as ExportDllAttribute.ExportDllAttribute;
							if (!dictionary.ContainsKey(type.FullName))
							{
								dictionary[type.FullName] = new Dictionary<string, KeyValuePair<string, string>>();
							}
							dictionary[type.FullName][memberInfo.Name] = new KeyValuePair<string, string>(exportDllAttribute.ExportName, exportDllAttribute.CallingConvention);
							num++;
						}
					}
				}
				if (num > 0)
				{
					int num2 = 1;
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
					Directory.SetCurrentDirectory(directoryName);
					Process process = new Process();
					string text2 = string.Format("/nobar{1}/out:{0}.il {0}.dll", fileNameWithoutExtension, flag ? " /linenum " : " ");
					Console.WriteLine("Deassebly file with arguments '{0}'", text2);
					process.StartInfo = new ProcessStartInfo(Settings.Default.ildasmpath, text2)
					{
						UseShellExecute = false,
						CreateNoWindow = false,
						RedirectStandardOutput = true,
						WorkingDirectory = directoryName
					};
					process.Start();
					process.WaitForExit();
					Console.WriteLine(process.StandardOutput.ReadToEnd());
					if (process.ExitCode != 0)
					{
						return process.ExitCode;
					}
					List<string> list = new List<string>();
					StreamReader streamReader = File.OpenText(Path.Combine(directoryName, fileNameWithoutExtension + ".il"));
					string text3 = "";
					string text4 = "";
					string text5 = "";
					string text6 = "";
					string text7 = "";
					int num3 = 0;
					Stack<string> stack = new Stack<string>();
					new List<string>();
					ParserState parserState = ParserState.Normal;
					while (!streamReader.EndOfStream)
					{
						string text8 = streamReader.ReadLine();
						string text9 = text8.Trim();
						bool flag2 = true;
						switch (parserState)
						{
						case ParserState.Normal:
							if (text9.StartsWith(".corflags"))
							{
								list.Add(".corflags 0x00000002");
								list.Add(string.Format(".vtfixup [{0}] int32 fromunmanaged at VT_01", num));
								list.Add(string.Format(".data VT_01 = int32[{0}]", num));
								Console.WriteLine("Adding vtfixup.");
								flag2 = false;
							}
							else if (text9.StartsWith(".class"))
							{
								parserState = ParserState.ClassDeclaration;
								flag2 = false;
								text5 = text9;
							}
							else if (text9.StartsWith(".assembly extern ExportDllAttribute"))
							{
								flag2 = false;
								parserState = ParserState.DeleteExportDependency;
								Console.WriteLine("Deleting ExportDllAttribute dependency.");
							}
							break;
						case ParserState.ClassDeclaration:
							if (text9.StartsWith("{"))
							{
								parserState = ParserState.Class;
								string text10 = "";
								Match match = new Regex(".+\\s+([^\\s]+) extends \\[.*").Match(text5);
								if (match.Groups.Count > 1)
								{
									text10 = match.Groups[1].Value;
								}
								text10 = text10.Replace("'", "");
								if (stack.Count > 0)
								{
									text10 = stack.Peek() + "+" + text10;
								}
								stack.Push(text10);
								Console.WriteLine("Found class: " + text10);
								list.Add(text5);
							}
							else
							{
								text5 = text5 + " " + text9;
								flag2 = false;
							}
							break;
						case ParserState.Class:
							if (text9.StartsWith(".class"))
							{
								parserState = ParserState.ClassDeclaration;
								flag2 = false;
								text5 = text9;
							}
							else if (text9.StartsWith(".method"))
							{
								if (dictionary.ContainsKey(stack.Peek()))
								{
									text3 = text9;
									flag2 = false;
									parserState = ParserState.MethodDeclaration;
								}
							}
							else if (text9.StartsWith("} // end of class"))
							{
								stack.Pop();
								if (stack.Count > 0)
								{
									parserState = ParserState.Class;
								}
								else
								{
									parserState = ParserState.Normal;
								}
							}
							break;
						case ParserState.DeleteExportDependency:
							if (text9.StartsWith("}"))
							{
								parserState = ParserState.Normal;
							}
							flag2 = false;
							break;
						case ParserState.MethodDeclaration:
							if (text9.StartsWith("{"))
							{
								Match match2 = new Regex("(?<before>[^\\(]+(\\(\\s[^\\)]+\\))*\\s)(?<method>[^\\(]+)(?<after>\\(.*)").Match(text3);
								if (match2.Groups.Count > 3)
								{
									text6 = match2.Groups["before"].Value;
									text7 = match2.Groups["after"].Value;
									text4 = match2.Groups["method"].Value;
								}
								Console.WriteLine("Found method: " + text4);
								if (dictionary[stack.Peek()].ContainsKey(text4))
								{
									num3 = list.Count;
									parserState = ParserState.MethodProperties;
								}
								else
								{
									list.Add(text3);
									parserState = ParserState.Method;
									num3 = 0;
								}
							}
							else
							{
								text3 = text3 + " " + text9;
								flag2 = false;
							}
							break;
						case ParserState.MethodProperties:
							if (text9.StartsWith(".custom instance void [ExportDllAttribute]"))
							{
								flag2 = false;
								parserState = ParserState.DeleteExportAttribute;
							}
							else if (text9.StartsWith("// Code"))
							{
								parserState = ParserState.Method;
								if (num3 != 0)
								{
									list.Insert(num3, text3);
								}
							}
							break;
						case ParserState.Method:
							if (text9.StartsWith("} // end of method"))
							{
								parserState = ParserState.Class;
							}
							break;
						case ParserState.DeleteExportAttribute:
							if (text9.StartsWith(".custom") || text9.StartsWith("// Code"))
							{
								KeyValuePair<string, string> keyValuePair = dictionary[stack.Peek()][text4];
								if (text6.Contains("marshal( "))
								{
									int startIndex = text6.IndexOf("marshal( ");
									text6 = text6.Insert(startIndex, "modopt([mscorlib]" + keyValuePair.Value + ") ");
									text3 = text6 + text4 + text7;
								}
								else
								{
									text3 = string.Concat(new string[]
									{
										text6,
										"modopt([mscorlib]",
										keyValuePair.Value,
										") ",
										text4,
										text7
									});
								}
								Console.WriteLine("\tChanging calling convention: " + keyValuePair.Value);
								if (num3 != 0)
								{
									list.Insert(num3, text3);
								}
								if (text4 == "DllMain")
								{
									list.Add(" .entrypoint");
								}
								list.Add(".vtentry 1 : " + num2.ToString());
								list.Add(string.Format(".export [{0}] as {1}", num2, dictionary[stack.Peek()][text4].Key));
								Console.WriteLine("\tAdding .vtentry:{0} .export:{1}", num2, dictionary[stack.Peek()][text4].Key);
								num2++;
								parserState = ParserState.Method;
							}
							else
							{
								flag2 = false;
							}
							break;
						}
						if (flag2)
						{
							list.Add(text8);
						}
					}
					streamReader.Close();
					StreamWriter streamWriter = File.CreateText(Path.Combine(directoryName, fileNameWithoutExtension + ".il"));
					foreach (string value in list)
					{
						streamWriter.WriteLine(value);
					}
					streamWriter.Close();
					string text11 = fileNameWithoutExtension + ".res";
					if (File.Exists(fileNameWithoutExtension + ".res"))
					{
						text11 = " /resource=" + text11;
					}
					else
					{
						text11 = "";
                    }
                    Directory.SetCurrentDirectory(directoryName);
                    process = new Process();
					text2 = string.Format("/nologo /quiet /out:_{0}.dll {0}.il /DLL{1} {2}", fileNameWithoutExtension, text11, flag ? "/debug" : "/optimize");
					Console.WriteLine("Compiling file with arguments '{0}'", text2);
					process.StartInfo = new ProcessStartInfo(Settings.Default.ilasmpath, text2)
					{
						UseShellExecute = false,
						CreateNoWindow = false,
						RedirectStandardOutput = true,
						WorkingDirectory = directoryName
					};
					process.Start();
					process.WaitForExit();
					Console.WriteLine(process.StandardOutput.ReadToEnd());
					if (process.ExitCode != 0)
					{
						return process.ExitCode;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return -1;
			}
			return 0;
		}
	}
}
