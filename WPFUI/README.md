# WPFUI

#### 介绍

WPF UI样式库

#### 软件架构

WPF

#### 引用其他开源项目说明

1.时间选择器来源于https://gitee.com/daiybh1/DateTimePicker?_from=gitee_search

#### 使用说明

1. 在项目中直接引用TemplateClassLibrary.dll动态链接库
2. 在.xmal文件中直接引用相应样式的命名空间
3. 引入资源字典
```
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="/TemplateClassLibrary;component/Template/Dictionary.xaml"/>
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

#### 参与贡献

1. Fork 本仓库
2. 新建 Feat_xxx 分支
3. 提交代码
4. 新建 Pull Request

#### UI展示

![button](https://images.gitee.com/uploads/images/2020/0922/091538_7280a71a_5672367.png "button.png")
![checkButton](https://images.gitee.com/uploads/images/2020/0922/091707_06883b0d_5672367.png "check.png")
![dateTimePicker](https://images.gitee.com/uploads/images/2020/0922/091728_fec5fad5_5672367.png "dateTimePicker.png")
![h](https://images.gitee.com/uploads/images/2020/0922/091751_c8e7bd50_5672367.png "h.png")
![图标字体](https://images.gitee.com/uploads/images/2020/0922/091806_b3558edb_5672367.png "icon.png")
![input](https://images.gitee.com/uploads/images/2020/0922/091824_6ba5da7c_5672367.png "input.png")
![input](https://images.gitee.com/uploads/images/2020/0922/091841_f0c7d66c_5672367.png "inputa.png")
![loading](https://images.gitee.com/uploads/images/2020/0922/091854_5da2f162_5672367.png "loading.png")
![radioButton](https://images.gitee.com/uploads/images/2020/0922/091908_8aa55de7_5672367.png "radio.png")
![table](https://images.gitee.com/uploads/images/2020/0922/091925_f2261f39_5672367.png "table.png")

#### 码云特技

1. 使用 Readme\_XXX.md 来支持不同的语言，例如 Readme\_en.md, Readme\_zh.md
2. 码云官方博客[blog.gitee.com](https://blog.gitee.com)
3. 你可以[https://gitee.com/explore](https://gitee.com/explore) 这个地址来了解码云上的优秀开源项目
4. [GVP](https://gitee.com/gvp) 全称是码云最有价值开源项目，是码云综合评定出的优秀开源项目
5. 码云官方提供的使用手册[https://gitee.com/help](https://gitee.com/help)
6. 码云封面人物是一档用来展示码云会员风采的栏目[https://gitee.com/gitee-stars/](https://gitee.com/gitee-stars/)

