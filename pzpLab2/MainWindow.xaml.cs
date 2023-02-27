using Microsoft.Win32;
using pzpLab2.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pzpLab2
{
    // •	CreateFileIn(char *fileName) – створення двійкового файлу даних, елементами якого є записи зі структурою, вказаною в індивідуальному варіанті.
    //  Створення нового файлу
    //  •	AddFileIn(char* fileName) – додавання записів у кінець вхідного файлу.
    //  [1] Збереження файлу
    //  •	ReadFileIn(char* fileName) – перегляд записів у вхідному файлі послідовно від першого до останнього.
    //  [2] Відкриття файлу
    //  •	CreateFileOut(char* fileName1, char* fileName2) – обробка даних із вхідного файлу по завданню індивідуального варіанту та виведення результатів у вихідний файл.
    //  Обробка відкритого файлу, [1] збереження нового за результатом роботи
    //  •	ReadFileOut(char* fileName) – перегляд записів у вихідному файлі послідовно від першого до останнього.
    //  [2] Відкриття файлу
    //  •	Дві функції сортування, які сортують вхідний файл до двом полям (поля вибрати по бажанню).
    
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string NEW_FILE_DEFAULT_NAME = "New File";
        private string _currentFile = NEW_FILE_DEFAULT_NAME;
        private List<Student> _studentList = new List<Student>();

        public MainWindow()
        {
            InitializeComponent();
            //List<Student> students = new List<Student>();
            //Student student = new Student("aba", 2001, "aaaa", 80f);
            //Student student1 = new Student("aaa", 2001, "aaaa", 80f);
            //Student student2 = new Student("aba", 2000, "abba", 80f);
            //Student student3 = new Student("ccc", 2000, "aaaa", 71.4f);
            //students.Add(student);
            //students.Add(student1);
            //students.Add(student2);
            //students.Add(student3);
            
            //_studentList = students.SortByGroupAndGpa();
            //RenderStudents(_studentList);
        }

        /// <summary>
        /// створення двійкового файлу даних, елементами якого є записи зі структурою, вказаною в індивідуальному варіанті.
        /// </summary>
        /// <param name="fileName"></param>
        private void CreateFileIn(string fileName)
        {
            _studentList.Clear();

            contentTextBox.Text = "";

            _currentFile = NEW_FILE_DEFAULT_NAME;
            Title = _currentFile;
        }

        /// <summary>
        /// додавання записів у кінець вхідного файлу.
        /// </summary>
        /// <param name="fileName"></param>
        private void AddFileIn(string fileName)
        {
            FileController.SaveStudentFile(fileName, _studentList);
        }

        /// <summary>
        /// перегляд записів у вхідному файлі послідовно від першого до останнього.
        /// </summary>
        private void ReadFileIn()
        {
            OpenFile();
        }

        /// <summary>
        /// обробка даних із вхідного файлу по завданню індивідуального варіанту та виведення результатів у вихідний файл.
        /// у вихідний файл записати відомості про тих студентів, які серед наймолодших мають максимальний середній бал.
        /// </summary>
        private void CreateFileOut()
        {
            List<Student> newStudents = _studentList.GetYoungestAndHighestGpa();
            if (newStudents == null || newStudents.Count() == 0)
            {
                return;
            }
            _studentList = newStudents;
            RenderStudents(_studentList);
            SaveFileAs();
        }

        /// <summary>
        /// перегляд записів у вихідному файлі послідовно від першого до останнього.
        /// </summary>
        private void ReadFileOut()
        {
            OpenFile();
        }

        private void SaveFileAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Student file (*.st)|*.st";
            if (saveFileDialog.ShowDialog() == true)
            {
                FileController.SaveStudentFile(saveFileDialog.FileName, _studentList);
                _currentFile = saveFileDialog.FileName;
            }
        }

        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                _studentList = FileController.OpenStudentFile(openFileDialog.FileName);
                contentTextBox.Text = StudentParser.Parse(_studentList);
                Title = openFileDialog.FileName;
            }
        }

        // Menu item listeners.

        private void OpenFileClick(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }

        private void CreateFileClick(object sender, RoutedEventArgs e)
        {
            CreateFileIn(_currentFile);
        }

        private void SaveFileClick(object sender, RoutedEventArgs e)
        {
            if (_currentFile == NEW_FILE_DEFAULT_NAME)
            {
                SaveFileAs();
            }
            else
            {
                AddFileIn(_currentFile);
            }
        }

        private void SaveFileAsClick(object sender, RoutedEventArgs e)
        {
            SaveFileAs();
        }

        private void AddStudentButtonClick(object sender, RoutedEventArgs e)
        {
            // Collect data 
            Student student = CollectStudentData();

            if (student == null)
            {
                MessageBox.Show("Incorrect student data");
                return;
            }
            // Add student
            _studentList.Add(student);
            // render student
            RenderStudents(_studentList);
            // Hide panel
            addStudentGrid.Visibility = Visibility.Collapsed;
        }

        private void AddNewStudentClick(object sender, RoutedEventArgs e)
        {
            addStudentGrid.Visibility = Visibility.Visible;
        }

        private void CreateFileOutClick(object sender, RoutedEventArgs e)
        {
            CreateFileOut();
        }

        private void SortByNameAndGroupClick(object sender, RoutedEventArgs e)
        {
            if (_studentList.Count() < 2)
            {
                return;
            }
            _studentList = _studentList.SortByNameAndGroup();
            RenderStudents(_studentList);
        }

        private void SortByGroupAndGpaClick(object sender, RoutedEventArgs e)
        {
            if (_studentList.Count() < 2)
            {
                return;
            }
            _studentList = _studentList.SortByGroupAndGpa();
            RenderStudents(_studentList);
        }

        // Utility.

        private void RenderStudents(List<Student> list)
        {
            contentTextBox.Text = StudentParser.Parse(list);
        }

        private Student CollectStudentData()
        {
            string name = nameTextBox.Text;
            string group = groupTextBox.Text;

            int yob = -1;
            bool yobSuccess = int.TryParse(yobTextBox.Text, out yob);
            int minAge = 10;
            if (yob < 1920 || (DateTime.Now.Year - yob) < minAge)
            {
                yobSuccess = false;
            }
            float gpa = -1f;
            bool gpaSuccess = float.TryParse(gpaTextBox.Text, out gpa);
            if (gpa < 0|| gpa > 100)
            {
                gpaSuccess = false;
            }

            if (name.Trim().Length == 0 || group.Trim().Length == 0 || !yobSuccess || !gpaSuccess)
            {
                return null;
            }

            return new Student(name, yob, group, gpa);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CharacterValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^a-zA-Za-åa-ö-w-я]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
