using System.Net.Http.Json;
using WinFormsApp1.models;

namespace WinFormsApp1
{
    public partial class AddForm : Form
    {
        HttpClient httpClient;
        public AddForm()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44373/");
        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void Form1_Load(object sender, EventArgs e)
        {

            HttpResponseMessage response = await httpClient.GetAsync("api/Course");
            if (response.IsSuccessStatusCode)
            {
                List<Course> courses = await response.Content.ReadAsAsync<List<Course>>();
                dgv1.DataSource = courses;

            }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            Course crs = new Course
            {
                name = txt_name.Text,
                description = txt_desc.Text,
                duration = int.Parse(txt_duraion.Text),
            };
            HttpResponseMessage message = await httpClient.PostAsJsonAsync("api/Course", crs);
            if (message.IsSuccessStatusCode)
            {
                Form1_Load(null, null);
                MessageBox.Show("Added", "Message", MessageBoxButtons.OK);
            }
        }
    }
}
