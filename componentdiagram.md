componentDiagram
    %% Presentation Layer
    component [UI: BaistClubAutomation.Web] as UI
    
    %% Business Logic Layer (BLL)
    component [BLL: BaistMasterService] as BLL
    
    %% Data Access Layer (DAL)
    component [DAL: BaistMasterManager] as DAL

    %% Database
    database [SQL Server: sarumugam3] as DB

    %% Interfaces (Lollipops & Sockets)
    UI --( IScoringService
    IScoringService -- BLL
    
    UI --( IMembershipService
    IMembershipService -- BLL
    
    UI --( ITeeTimeService
    ITeeTimeService -- BLL

    BLL --( IScoreManager
    IScoreManager -- DAL

    BLL --( IMembershipManager
    IMembershipManager -- DAL

    BLL --( ITeeTimeManager
    ITeeTimeManager -- DAL

    %% Physical Database Connection
    DAL ..> DB : ADO.NET / EF Core