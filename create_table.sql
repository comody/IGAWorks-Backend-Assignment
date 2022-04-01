CREATE TABLE `event_database`.`event_tb` (
  `event_id` VARCHAR(50) NOT NULL,
  `user_id` VARCHAR(50) NOT NULL,
  `event` VARCHAR(50) NULL,
  `parameters` BLOB NULL,
  `event_datetime` DATETIME(3) NULL,
  PRIMARY KEY (`event_id`, `user_id`));