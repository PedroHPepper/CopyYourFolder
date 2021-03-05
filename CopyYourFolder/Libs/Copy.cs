using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CopyYourFolder.Libs
{
    public class Copy
    {
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            var dir = new DirectoryInfo(sourceDirName);
            var DirSubDirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                const string message =
                    "Não foi possível encontrar o diretório de origem.";
                const string caption = "Erro";
                MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK);
                return;
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the file contents of the directory to copy.
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                // Create the path to the new copy of the file.
                var temppath = Path.Combine(destDirName, file.Name);
                if (File.Exists(temppath))
                {
                    string message =
                    "Já foi encontrado um arquivo com o nome de "+ file.Name + ". Deseja substituí-lo?";
                    const string caption = "Erro";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        File.Delete(temppath);
                        file.CopyTo(temppath, true);
                    }
                }
                else
                {
                    // Copy the file.
                    file.CopyTo(temppath, true);
                }
                

            }

            // If copySubDirs is true, copy the subdirectories.
            if (!copySubDirs) return;

            foreach (var subdir in DirSubDirs)
            {
                // Create the subdirectory.
                var temppath = Path.Combine(destDirName, subdir.Name);

                // Copy the subdirectories.
                DirectoryCopy(subdir.FullName, temppath, copySubDirs);
            }
        }
    }
}
