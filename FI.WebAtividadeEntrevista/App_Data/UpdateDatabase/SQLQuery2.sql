If not exists (select * from INFORMATION_SCHEMA.columns where table_name = 'CLIENTES' and column_name = 'CPF')
BEGIN
    ALTER TABLE CLIENTES ADD CPF VARCHAR(14) NOT NULL
END 