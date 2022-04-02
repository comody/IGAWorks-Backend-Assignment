CREATE TABLE `event_database`.`event_tb` (
  `event_id` VARCHAR(50) NOT NULL,
  `user_id` VARCHAR(50) NOT NULL,
  `event` VARCHAR(50) NOT NULL,
  `parameters` BLOB NULL,
  `event_datetime` DATETIME(3) NOT NULL,
  PRIMARY KEY (`event_id`));

ALTER TABLE event_tb ADD INDEX idx_user(user_id);
