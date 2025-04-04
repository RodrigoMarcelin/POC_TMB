CREATE EXTENSION IF NOT EXISTS "pgcrypto";

CREATE TABLE IF NOT EXISTS "Order" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "Cliente" VARCHAR(100) NOT NULL,
    "Produto" VARCHAR(100) NOT NULL,
    "Valor" DECIMAL(10, 2) NOT NULL,
    "Status" VARCHAR(50) NOT NULL,
    "DataCriacao" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
); 

-- COMMENT ON COLUMN "Order".Status IS E'- Pendente\n- Processando\n- Finalizado';