create sequence sq_ia_cs_log;
create table ia_cs_log (
  logid number not null
 ,app varchar2(50)
 ,logged timestamp
 ,lvl varchar2(50)
 ,message varchar2(4000)
 ,logger varchar2(250)
 ,constraint pk_ia_cs_log primary key (logid)
)
partition by range(logged) interval (numtoyminterval(3,'month'))
  (partition p_2017_q1 values less than (to_date('01.04.2017','dd.mm.yyyy'))
  ,partition p_2017_q2 values less than (to_date('01.07.2017','dd.mm.yyyy'))
  ,partition p_2017_q3 values less than (to_date('01.10.2017','dd.mm.yyyy'))
  ,partition p_2017_q4 values less than (to_date('01.01.2018','dd.mm.yyyy'))
  --,partition p_default values less than (maxvalue)
  );
comment on table ia_cs_log is 'Журнал приложений C#';
comment on column ia_cs_log.logid is 'Идентификатор';
comment on column ia_cs_log.app is 'Приложение';
comment on column ia_cs_log.logged is 'Дата и время';
comment on column ia_cs_log.lvl is 'Уровень';
comment on column ia_cs_log.message is 'Сообщение';
comment on column ia_cs_log.logger is 'Логгер';