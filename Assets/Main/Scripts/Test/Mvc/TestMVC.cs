using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TestMVC : BaseUpdateController<TestView, TestModel>
{
    public override void UpdateView()
    {
        View.Text.text = Model.testCount.ToString();
    }

    protected override void OnShow()
    {
        View.button.onClick.AddListener(OnClick);

        View.Text.text = "Ураа, всем привет!";
    }

    private void OnClick()
    {
        Model.testCount += 1;

        UpdateView();
    }
}

public class TestModel : IModel
{
    public int testCount;

    public void GetData()
    {
        throw new NotImplementedException();
    }

    public void UpdateData()
    {
        throw new NotImplementedException();
    }
}
