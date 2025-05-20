using System.Collections.ObjectModel;
using System.ComponentModel;
using VideocartLab.MainModelsProj.Screen;

namespace VideocartLab.ModelViews;

/// <summary>
/// Информация о настройках экрана
/// </summary>
public class ScreenConnectionInfo
{
    /// <summary>
    /// Имя настройки
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Настройка экрана
    /// </summary>
    internal ScreenInterface? ScreenInterface { get; set; }
}

/// <summary>
/// Интерфейс подключения к эрану
/// </summary>
public class ScreenInterfaceViewModel : ModelViewBase
{
    private ObservableCollection<ScreenConnectionInfo> standartSettings = new();
    private ScreenConnectionInfo? setting = null;

    //Необходимо чтобы не происходил сброс выбранного интерфейса
    private bool selectedIsIniciator = false;

    private int bitPerPixel = 8;
    private double bandwidth = 1;
    private double frequency = 1;
    private int screenWidth = 100;
    private int screenHeight = 100;

    public ScreenInterfaceViewModel()
    {
        InitList();
        SelectedSetting = StandartSettings[0];
    }

    private void InitList()
    {
        #region adding

        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "Пользовательское",
            ScreenInterface = null
        });

        #region hdmi
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "HDMI 1.3 | 1080p@144",
            ScreenInterface = HDMI.HDMI1dot3_1080p
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "HDMI 1.3 | 720p@144",
            ScreenInterface = HDMI.HDMI1dot3_720p
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "HDMI 1.3 | 4k@30",
            ScreenInterface = HDMI.HDMI1dot3_4k
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "HDMI 2.1 | 8k@30",
            ScreenInterface = HDMI.HDMI2dot1_8k
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "HDMI 2.1 | 4k@144",
            ScreenInterface = HDMI.HDMI2dot1_4k
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "HDMI 2.1 | 1080p@240",
            ScreenInterface = HDMI.HDMI2dot1_1080p
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "HDMI 2.1 | 720p@240",
            ScreenInterface = HDMI.HDMI2dot1_720p
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "HDMI 2.0 | 1080p@240",
            ScreenInterface = HDMI.HDMI2_1080p
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "HDMI 2.0 | 720p@240",
            ScreenInterface = HDMI.HDMI2_720p
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "HDMI 2.0 | 4k@60",
            ScreenInterface = HDMI.HDMI2_4k
        });
        #endregion

        #region displayport
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "DP 1.0 | 1080p@144",
            ScreenInterface = DisplayPort.DP1_1080p
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "DP 1.0 | 720p@144",
            ScreenInterface = DisplayPort.DP1_720p
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "DP 1.0 | 4k@30",
            ScreenInterface = DisplayPort.DP1_4k
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "DP 1.2 | 1080p@240",
            ScreenInterface = DisplayPort.DP1dot2_1080p
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "DP 1.2 | 720p@240",
            ScreenInterface = DisplayPort.DP1dot2_720p
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "DP 1.2 | 4k@75",
            ScreenInterface = DisplayPort.DP1dot2_4k
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "DP 1.4 | 1080p@360",
            ScreenInterface = DisplayPort.DP1dot4_1080p
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "DP 1.4 | 720p@360",
            ScreenInterface = DisplayPort.DP1dot4_720p
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "DP 1.4 | 4k@120",
            ScreenInterface = DisplayPort.DP1dot4_4k
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "DP 1.4 | 8k@30",
            ScreenInterface = DisplayPort.DP1dot4_8k
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "DP 2.0 | 720p@240",
            ScreenInterface = DisplayPort.DP2_720p
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "DP 2.0 | 1080p@240",
            ScreenInterface = DisplayPort.DP2_1080p
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "Dp 2.0 | 4k@240",
            ScreenInterface = DisplayPort.DP2_4k
        });
        standartSettings.Add(new ScreenConnectionInfo()
        {
            Name = "DP 2.0 | 8k@85",
            ScreenInterface = DisplayPort.DP2_8k
        });
        #endregion

        #endregion
    }

    /// <summary>
    /// Список стандратных настроек экрана
    /// </summary>
    public ObservableCollection<ScreenConnectionInfo> StandartSettings => standartSettings;

    /// <summary>
    /// Выбранная настройка экрана
    /// </summary>
    public ScreenConnectionInfo? SelectedSetting
    {
        get => setting;
        set
        {
            if (setting == value)
                return;
            setting = value;

            selectedIsIniciator = true;

            if (setting != StandartSettings[0])
            {
                ScreenInterface info = SelectedSetting!.ScreenInterface!;

                //selectedIsIniciator = false;

                this.Bandwidth = info.Bandwidth;
                this.BitPerPixel = info.BitPerPixel;
                this.Frequency = info.Frequency;
                this.ScreenHeight = info.MaxHeight;
                this.ScreenWidth = info.MaxWidth;
            }
            OnPropertyChanged();
            selectedIsIniciator = false;
        }
    }

    #region StandartProperties
    /// <summary>
    /// Частота экрана [Гц]
    /// </summary>
    public double? Frequency
    {
        get => frequency;
        set
        {
            frequency = value ?? 0d;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Пропускная способность экрана [Гбит/с]
    /// </summary>
    public double? Bandwidth
    {
        get => bandwidth;
        set
        {
            bandwidth = value ?? 0d;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Глубина цвета [бит]
    /// </summary>
    public int? BitPerPixel
    {
        get => bitPerPixel;
        set
        {
            bitPerPixel = value ?? 0;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Ширина экрана
    /// </summary>
    public int? ScreenWidth
    {
        get => screenWidth;
        set
        {
            screenWidth = value ?? 0;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Высота экрана
    /// </summary>
    public int? ScreenHeight
    {
        get => screenHeight;
        set
        {
            screenHeight = value ?? 0;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Требуемая пропускная способность для работы экрана
    /// </summary>
    public double RequiredBandwidth
    {
        get => ScreenWidth!.Value * ScreenHeight!.Value *
            BitPerPixel!.Value * 3d * Frequency!.Value / 1024d / 1024d / 1024d;
    }
    #endregion

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        //Если меняется какое-либо из свойств, кроме этих,
        //то меняется требуемая пропускная способность
        if (e.PropertyName == nameof(RequiredBandwidth)
            || e.PropertyName == nameof(Bandwidth)
            || e.PropertyName == nameof(SelectedSetting))
            goto end;

        OnPropertyChanged(nameof(RequiredBandwidth));
        return;
end:
//Сброс стандартных настроек до пользовательских
        if (!selectedIsIniciator)
        {
            SelectedSetting = StandartSettings[0];
        }
    }
}



