
INSERT INTO companies (name)
SELECT v.name
FROM (VALUES
    ('Ipiranga TRR'), 
    ('Vibra TRR'), 
    ('Raizen TRR')
) AS v(name)
WHERE NOT EXISTS (
    SELECT 1 FROM companies c WHERE c.name = v.name
);

INSERT INTO bases (company_id, name, alter_name, alter_name2, location, state) 
SELECT c.id, new_data.name, new_data.alter_name, new_data.alter_name2, new_data.location, new_data.state 
FROM companies c
JOIN (
    VALUES
        ('Coord. Nucleo De Canoas TRR', 'Coord Nucleo De Canoas TRR', 'Coord Nucleo Canoas TRR', 'Canoas', 'RS'),
        ('Escritorio De Rio Grande TRR', 'Escritorio Rio Grande TRR', '', 'Rio Grande TRR', 'RS'),
        ('Escritorio de Passo Fundo TRR', 'Escritorio De Passo Fundo TRR', '', 'Passo Fundo TRR', 'RS')    
) AS new_data (name, alter_name, alter_name2, location, state)
ON c.name = 'Ipiranga TRR'
WHERE NOT EXISTS (
    SELECT 1
    FROM bases b
    WHERE b.company_id = c.id AND b.name = new_data.name
);

INSERT INTO bases (company_id, name, alter_name, alter_name2, location, state) 
SELECT c.id, new_data.name, new_data.alter_name, new_data.alter_name2, new_data.location, new_data.state 
FROM companies c
JOIN (
    VALUES
        ('ARRIG TRR', '5067', '', 'Rio Grande', 'RS'),
        ('BANOAS TRR', '5068', '', 'Canoas', 'RS'),
        ('AIPAF TRR', '5283', '', 'Passo Fundo', 'RS'),
        ('BAJUI TRR', '5286', '', 'Ijui', 'RS')    
) AS new_data (name, alter_name, alter_name2, location, state)
ON c.name = 'Vibra TRR'
WHERE NOT EXISTS (
    SELECT 1 
    FROM bases b 
    WHERE b.company_id = c.id AND b.name = new_data.name
);

INSERT INTO bases (company_id, name, alter_name, alter_name2, location, state) 
SELECT c.id, new_data.name, new_data.alter_name, new_data.alter_name2, new_data.location, new_data.state 
FROM companies c
JOIN (
    VALUES
        ('BEST TRR', 'BEST-BASE ESTEIO', '', 'Esteio', 'RS'),
        ('BPFU TRR', 'BPFU-PASSO FUNDO', '', 'Passo Fundo', 'RS'),
        ('BIJI TRR', 'BIJI-IJIU', '', 'Ijui', 'RS'),    
        ('BGRD TRR', 'BGDR-RIO GRANDE', '', 'Rio Grande', 'RS')
) AS new_data (name, alter_name, alter_name2, location, state)
ON c.name = 'Raizen TRR'
WHERE NOT EXISTS (
    SELECT 1 
    FROM bases b 
    WHERE b.company_id = c.id AND b.name = new_data.name
);

INSERT INTO common_products (name, description)
SELECT new_data.name, new_data.description
FROM (
    VALUES
        ('Od.BS500', ''),
        ('Od.BS500Adt', ''),
        ('Od.BS10', ''),
        ('Od.BS10Adt', ''),
        ('Diesel Mar.', ''),
        ('Oc.B1(BPF)', ''),
        ('Oc.P1(BTE)', ''),
        ('Xisto', ''),
        ('Od.BS500.Inv', ''),
        ('Od.BS50.Inv', '')
) AS new_data(name, description)
WHERE NOT EXISTS (
    SELECT 1
    FROM common_products cp
    WHERE cp.name = new_data.name
);

INSERT INTO products (common_product_id, company_id, name)
SELECT cp.id, c.id, new_data.name
FROM (
    VALUES    
        ('Od.BS500'), 
        ('Od.BS500Adt'),
        ('Od.BS10'),
        ('Od.BS10Adt'),
        ('Diesel Mar.'),
        ('Oc.B1(BPF)'),
        ('Oc.P1(BTE)'),
        ('Xisto'),
        ('Od.BS500.Inv'),
        ('Od.BS50.Inv')
) AS new_data(name)
JOIN companies c ON c.name = 'Ipiranga TRR'
JOIN common_products cp ON cp.name = new_data.name
WHERE NOT EXISTS (
    SELECT 1
    FROM products p
    WHERE p.company_id = c.id AND p.name = new_data.name
);

INSERT INTO products (common_product_id, company_id, name)
SELECT cp.id, c.id, new_data.name
FROM (
    VALUES    
        ('Od.BS500'), 
        ('Od.BS500Adt'),
        ('Od.BS10'),
        ('Od.BS10Adt'),
        ('Diesel Mar.'),
        ('Oc.B1(BPF)'),
        ('Oc.P1(BTE)'),
        ('Xisto'),
        ('Od.BS500.Inv'),
        ('Od.BS50.Inv')
) AS new_data(name)
JOIN companies c ON c.name = 'Vibra TRR'
JOIN common_products cp ON cp.name = new_data.name
WHERE NOT EXISTS (
    SELECT 1
    FROM products p
    WHERE p.company_id = c.id AND p.name = new_data.name
);

INSERT INTO products (common_product_id, company_id, name)
SELECT cp.id, c.id, new_data.name
FROM (
    VALUES    
        ('Od.BS500'), 
        ('Od.BS500Adt'),
        ('Od.BS10'),
        ('Od.BS10Adt'),
        ('Diesel Mar.'),
        ('Oc.B1(BPF)'),
        ('Oc.P1(BTE)'),
        ('Xisto'),
        ('Od.BS500.Inv'),
        ('Od.BS50.Inv')
) AS new_data(name)
JOIN companies c ON c.name = 'Raizen TRR'
JOIN common_products cp ON cp.name = new_data.name
WHERE NOT EXISTS (
    SELECT 1
    FROM products p
    WHERE p.company_id = c.id AND p.name = new_data.name
);

INSERT INTO stations (company_id, subsidiary_number, name, registration_number, company_code, city, state) 
SELECT c.id, new_data.subsidiary_number, new_data.name, new_data.registration_number, new_data.company_code, new_data.city, new_data.state
FROM companies c
JOIN (
    VALUES
    ('7',    'Rio Grande',          '93489243000701', '6186610',    'Rio Grande',           'RS'),
    ('12',   'Rio Grande',          '93489243001279', '613697',     'Rio Grande',           'RS'),
    ('37',   'Canoas',              '93489243003727', '6209858',    'Canoas',               'RS'),
    ('38',   'Canoas',              '93489243003808', '6209955',    'Canoas',               'RS'),
    ('56',   'Canoas',              '93489243005690', '6829007',    'Canoas',               'RS'),
    ('57',   'São Leopoldo',        '93489243005770', '6828922',    'São Leopoldo',         'RS'),
    ('59',   'Rio Grande',          '93489243005932', '6827829',	'Rio Grande',           'RS'),
    ('61',	'Cachoeirinha',         '93489243006157', '6829015',	'Cachoeirinha',         'RS'),
    ('80',	'Eldorado do Sul',      '93489243008010', '6136907',	'Eldorado do Sul',      'RS'),
    ('90',	'São Gabriel',          '93489243009091', '7066074',	'São Gabriel',          'RS'),
    ('91',	'Pelotas',              '93489243009172', '7038038',	'Pelotas',              'RS')
) AS new_data (subsidiary_number, name, registration_number, company_code, city, state)
ON c.name = 'Ipiranga TRR';

INSERT INTO stations (company_id, subsidiary_number, name, registration_number, company_code, city, state) 
SELECT c.id, new_data.subsidiary_number, new_data.name, new_data.registration_number, new_data.company_code, new_data.city, new_data.state
FROM companies c
JOIN (
    VALUES
    ('7',    'Rio Grande',          '93489243000701', '6186610',    'Rio Grande',           'RS'),
    ('12',   'Rio Grande',          '93489243001279', '613697',     'Rio Grande',           'RS'),
    ('37',   'Canoas',              '93489243003727', '6209858',    'Canoas',               'RS'),
    ('38',   'Canoas',              '93489243003808', '6209955',    'Canoas',               'RS'),
    ('56',   'Canoas',              '93489243005690', '6829007',    'Canoas',               'RS'),
    ('57',   'São Leopoldo',        '93489243005770', '6828922',    'São Leopoldo',         'RS'),
    ('59',   'Rio Grande',          '93489243005932', '6827829',	'Rio Grande',           'RS'),
    ('61',	'Cachoeirinha',         '93489243006157', '6829015',	'Cachoeirinha',         'RS'),
    ('80',	'Eldorado do Sul',      '93489243008010', '6136907',	'Eldorado do Sul',      'RS'),
    ('90',	'São Gabriel',          '93489243009091', '7066074',	'São Gabriel',          'RS'),
    ('91',	'Pelotas',              '93489243009172', '7038038',	'Pelotas',              'RS')
) AS new_data (subsidiary_number, name, registration_number, company_code, city, state)
ON c.name = 'Vibra TRR';

INSERT INTO stations (company_id, subsidiary_number, name, registration_number, company_code, city, state) 
SELECT c.id, new_data.subsidiary_number, new_data.name, new_data.registration_number, new_data.company_code, new_data.city, new_data.state
FROM companies c
JOIN (
    VALUES
    ('7',    'Rio Grande',          '93489243000701', '6186610',    'Rio Grande',           'RS'),
    ('12',   'Rio Grande',          '93489243001279', '613697',     'Rio Grande',           'RS'),
    ('37',   'Canoas',              '93489243003727', '6209858',    'Canoas',               'RS'),
    ('38',   'Canoas',              '93489243003808', '6209955',    'Canoas',               'RS'),
    ('56',   'Canoas',              '93489243005690', '6829007',    'Canoas',               'RS'),
    ('57',   'São Leopoldo',        '93489243005770', '6828922',    'São Leopoldo',         'RS'),
    ('59',   'Rio Grande',          '93489243005932', '6827829',	'Rio Grande',           'RS'),
    ('61',	'Cachoeirinha',         '93489243006157', '6829015',	'Cachoeirinha',         'RS'),
    ('80',	'Eldorado do Sul',      '93489243008010', '6136907',	'Eldorado do Sul',      'RS'),
    ('90',	'São Gabriel',          '93489243009091', '7066074',	'São Gabriel',          'RS'),
    ('91',	'Pelotas',              '93489243009172', '7038038',	'Pelotas',              'RS')
) AS new_data (subsidiary_number, name, registration_number, company_code, city, state)
ON c.name = 'Raizen TRR';

ALTER TABLE public."prices" ADD COLUMN if not exists "frete" DECIMAL(10, 5);

update "bases" set "name" = 'Coord. Nucleo De Canoas TRR' where "name" = 'Coord. Nucleo De Canoas' and "company_id" =7;
update "bases" set "name" = 'Escritorio De Rio Grande TRR' where "name" = 'Escritorio De Rio Grande' and "company_id" =7;
update "bases" set "name" = 'Escritorio de Passo Fundo TRR' where "name" = 'Escritorio de Passo Fundo' and "company_id" =7;

update "bases" set "name" = 'ARRIG TRR' where "name" = 'ARRIG' and "company_id" =6;
update "bases" set "name" = 'BANOAS TRR' where "name" = 'BANOAS' and "company_id" =6;
update "bases" set "name" = 'AIPAF TRR' where "name" = 'AIPAF TRR' and "company_id" =6;
update "bases" set "name" = 'BAJUI TRR' where "name" = 'BAJUI TRR' and "company_id" =6;

update "bases" set "name" = 'BEST TRR' where "name" = 'BEST' and "company_id" =8;
update "bases" set "name" = 'BPFU TRR' where "name" = 'BPFU' and "company_id" =8;
update "bases" set "name" = 'BIJI TRR' where "name" = 'BIJI' and "company_id" =8;
update "bases" set "name" = 'BGRD TRR' where "name" = 'BGRD' and "company_id" =8;
