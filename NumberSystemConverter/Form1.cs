namespace NumberSystemConverter
{
    public partial class Form1 : Form
    {
        private long tempNum;
        private int inNumSys;
        private int outNumSys;
        private bool minusNum;
        public bool decimalNum;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CurrentData())
            {
                ConvertToDecimal();
                ConvertToNeedSys();
                DeleteNumSys();
            }
        }
        private bool CurrentData()
        {
            // ���� �� ��������
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("������� ��������� ��������");
                return false;
            }
            // ������� �/c
            try
            {
                inNumSys = Convert.ToInt32(comboBox1.Text);
                outNumSys = Convert.ToInt32(comboBox2.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("�������� ������� ���������");
                return false;
            }
            // ������������� �� �����
            if (textBox1.Text[0] == '-')
            {
                minusNum = true;
            }
            // �������� �� ������������ ������


            char[] arr = new char[inNumSys];
            if (inNumSys > 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    arr[i] = (char)(i + 48);
                }
                for (int i = 10; i < inNumSys; i++)
                {
                    arr[i] = (char)(i + 55);
                }
            }
            else
            {
                for (int i = 0; i < inNumSys; i++)
                {
                    arr[i] = (char)(i + 48);
                }
            }


            for (int i = minusNum ? 1: 0; i < textBox1.Text.Length; i++)
            {
                int check = 0;
                for (int j =0; j < arr.Length; j++)
                {
                    if (arr[j] == textBox1.Text[i])
                    {
                        check++;
                        break;
                    }
                }
                if (check == 0)
                {
                    MessageBox.Show("������������ ������");
                    return false;
                }
            }
            // ���� ����� ������� ������� + ���� ����� = 10
            if (inNumSys == 10)
            {
                try
                {
                    tempNum = Convert.ToInt64(textBox1.Text);
                    decimalNum = true;
                    return true;
                }
                catch (Exception)
                {
                    MessageBox.Show("������� ������� �����");
                    return false;
                }
            }
            // ������������� ��������������
            if (inNumSys == outNumSys)
            {
                MessageBox.Show("� ��� �����?");
                return false;
            }
            
            return true;
        }
        private void ConvertToDecimal()
        {
            if (decimalNum)
            {
                return;
            }
            string str = textBox1.Text;
            char[] arr = new char[inNumSys];
            if (inNumSys > 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    arr[i] = (char)(i + 48);
                }
                for (int i = 10; i < inNumSys; i++)
                {
                    arr[i] = (char)(i + 55);
                }
            }
            else
            {
                for (int i = 0; i < inNumSys; i++)
                {
                    arr[i] = (char)(i + 48);
                }
            }
            int power = 0;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                for (int j = 0; j < inNumSys; j++)
                {
                    if (arr[j] == str[i] || (char)(j + 87) == str[i])
                    {
                        tempNum += j * (int)Math.Pow(inNumSys, power);
                        power++;
                        break;
                    }
                }
            }
        }
        private void ConvertToNeedSys()
        {
            string str1 = "";
            while (tempNum != 0)
            {
                long temp = 0;
                temp = tempNum % outNumSys;
                if (temp < 10)
                {
                    str1 += (char)(temp + 48);
                }
                else
                {
                    str1 += (char)(temp + 55);
                }
                tempNum /= outNumSys;
            }
            if (outNumSys == 2 && minusNum)
            {
                string str2 = str1;
                if (minusNum)
                {
                    str1 += '-';
                }
                textBox2.Text = new string(str1.ToCharArray().Reverse().ToArray());
                while (str2.Length % 8 != 7)
                {
                    str2 += '0';
                }
                str2 += '1';
                textBox2.Text += " �� " + new string(str2.ToCharArray().Reverse().ToArray());
            }
            else
            {
                if (minusNum)
                {
                    str1 += '-';
                }
                string str = new string(str1.ToCharArray().Reverse().ToArray());
                textBox2.Text = str;
            }
            for (int i = 0; i < textBox2.Text.Length; i++)
            {
                if (textBox2.Text[i] == '+' || textBox2.Text[i] == '/'  || textBox2.Text[i] == '^')
                {
                    textBox2.Text = "";
                    MessageBox.Show("������� ������� �����");
                    break;
                }
            }
        }
        private void DeleteNumSys()
        {
            tempNum = 0;
            inNumSys = 0;
            outNumSys = 0;
            minusNum = false;
            decimalNum = false;
        }
    }
}