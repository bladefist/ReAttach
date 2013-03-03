using System;
using System.IO;

namespace ReAttach.Data
{
	public class ReAttachTarget
	{
		public int ProcessId { get; set; }
		public string ProcessName { get; private set; }
		public string ProcessPath { get; private set; }
		public string ProcessUser { get; private set; }
		public string ServerName { get; private set; }
		public bool IsAttached { get; set; }
		public bool IsLocal { get { return string.IsNullOrEmpty(ServerName); } }

		public ReAttachTarget(int pid, string path, string user, string serverName = "") 
		{
			try
			{
				ProcessName = Path.GetFileName(path);
			}
			catch
			{
				ProcessName = path;
			}
			ProcessId = pid;
			ProcessPath = path;
			ProcessUser = user;
			ServerName = serverName;
		}

		public override bool Equals(object obj)
		{
			var other = obj as ReAttachTarget;
			if (other == null)
				return false;
			return ProcessPath.Equals(other.ProcessPath, StringComparison.OrdinalIgnoreCase) &&
				ProcessUser.Equals(other.ProcessUser, StringComparison.OrdinalIgnoreCase) &&
				ServerName.Equals(other.ServerName, StringComparison.OrdinalIgnoreCase);
		}

		public override int GetHashCode()
		{
			return ProcessPath.ToLower().GetHashCode() + 
				ProcessUser.ToLower().GetHashCode() + 
				ServerName.ToLower().GetHashCode();
		}

		public override string ToString()
		{
			return IsLocal ? 
				string.Format("{0} ({1})", ProcessName, ProcessUser) : 
				string.Format("{0} ({1}@{2})", ProcessName, ProcessUser, ServerName);
		}
	}
}