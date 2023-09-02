-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 02-Set-2023 às 21:20
-- Versão do servidor: 10.4.24-MariaDB
-- versão do PHP: 8.1.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `tcc`
--
DROP DATABASE IF EXISTS `tcc`;
CREATE DATABASE IF NOT EXISTS `tcc` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `tcc`;

-- --------------------------------------------------------

--
-- Estrutura da tabela `perfil`
--

DROP TABLE IF EXISTS `perfil`;
CREATE TABLE `perfil` (
  `id` int(3) NOT NULL,
  `perfil` varchar(60) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Extraindo dados da tabela `perfil`
--

INSERT INTO `perfil` (`id`, `perfil`) VALUES
(1, 'cliente'),
(2, 'administrador'),
(3, 'gerente');

-- --------------------------------------------------------

--
-- Estrutura da tabela `produto`
--

DROP TABLE IF EXISTS `produto`;
CREATE TABLE `produto` (
  `idproduto` int(10) NOT NULL,
  `nome` varchar(60) NOT NULL,
  `valor` decimal(10,2) NOT NULL,
  `foto` varchar(60) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Extraindo dados da tabela `produto`
--

INSERT INTO `produto` (`idproduto`, `nome`, `valor`, `foto`) VALUES
(1, 'coca', '7.50', 'C:\\Users\\aluno\\Documents\\GitHub\\techittcc\\foto\\coca.jpg');

-- --------------------------------------------------------

--
-- Estrutura da tabela `usuario`
--

DROP TABLE IF EXISTS `usuario`;
CREATE TABLE `usuario` (
  `idusuario` int(6) NOT NULL,
  `nomeusuario` varchar(60) NOT NULL,
  `login` varchar(30) NOT NULL,
  `senha` varchar(60) NOT NULL,
  `perfil` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Extraindo dados da tabela `usuario`
--

INSERT INTO `usuario` (`idusuario`, `nomeusuario`, `login`, `senha`, `perfil`) VALUES
(1, 'jorge ciffoni', 'ciffoni', '8331e766a06eedd77bdc3afb4bdc4ad7', 1),
(3, 'amanda', 'aciffoni', 'aula123', 1);

--
-- Índices para tabelas despejadas
--

--
-- Índices para tabela `perfil`
--
ALTER TABLE `perfil`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `produto`
--
ALTER TABLE `produto`
  ADD PRIMARY KEY (`idproduto`);

--
-- Índices para tabela `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`idusuario`);

--
-- AUTO_INCREMENT de tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `perfil`
--
ALTER TABLE `perfil`
  MODIFY `id` int(3) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de tabela `produto`
--
ALTER TABLE `produto`
  MODIFY `idproduto` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de tabela `usuario`
--
ALTER TABLE `usuario`
  MODIFY `idusuario` int(6) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
